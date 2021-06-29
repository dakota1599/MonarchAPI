using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Cors;
using MonarchAPI.Models;
using MonarchAPI.Data;
using System.Security.Cryptography;
using System.Text;

namespace MonarchAPI.Controllers
{
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        protected MonarchContext _context { get; set; }

        public UserController(MonarchContext context) {
            this._context = context;
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateUser([FromBody] User user) {
            
            //Ensures that the queries are not empty.
            if (user == null) {
                return BadRequest();
            }

            //Gets a list of all users with that username to check.
            var users = await _context.Users
                .Where(u => u.UserName == user.UserName)
                .ToListAsync();

            //If that username does not yet exist, add the user.
            if (users.Count == 0)
            {
                //Encodes the password
                var bytes = Encoding.UTF8.GetBytes(user.Password);
                var md5 = MD5.Create();
                user.Password = BitConverter.ToString(md5.ComputeHash(bytes)).Replace("-", String.Empty);
                
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                //Return true to trigger front end continuation.
                return Json(true);
            }

            //If the username does exist, return false to trigger front end validation.
            return Json(false);
        }
    }
}
