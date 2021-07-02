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
    [ApiController]
    public class MemberController : Controller
    {
        protected MonarchContext _context { get; set; }

        public MemberController(MonarchContext context) {

            this._context = context;
        
        }

        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateMember([FromBody] Member member) {

            //Ensures that the queries are not empty.
            if (member == null)
            {
                return BadRequest();
            }

            //Gets a list of all users with that username to check.
            var members = await _context.Members
                .Where(m => m.UserName == member.UserName)
                .ToListAsync();

            //If that username does not yet exist, add the member.
            if (members.Count == 0)
            {
                //Encodes the password
                var bytes = Encoding.UTF8.GetBytes(member.Password);
                var md5 = MD5.Create();
                member.Password = BitConverter.ToString(md5.ComputeHash(bytes)).Replace("-", String.Empty);
                member.UserName = member.UserName.ToLower();

                _context.Members.Add(member);
                await _context.SaveChangesAsync();
                //Return true to trigger front end continuation.
                return Json(true);
            }

            //If the username does exist, return false to trigger front end validation.
            return Json(false);

        }

        //Gets the Members
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMember(int id) {

            //Gets the member with the associated id
            var member = await _context.Members
                .Where(m => m.ID == id)
                .FirstOrDefaultAsync();

            //If member is null, return false
            if (member == null) {
                return Json(false);
            }

            //Gets the list of meetings associated with that account
            var meetings = await _context.CheckIns
                .Where(m => m.MemberID == member.ID)
                .ToListAsync();

            //Sets the check in list to that list of checkings
            member.CheckIns = meetings;
            //Nullifies the password
            member.Password = null;
            //Retuns the member object.
            return Json(member);
        
        }

        [HttpGet]
        [Route("list/{id:int}")]
        public async Task<IActionResult> GetUserList(int id) { 
        
            var members = await _context.Members.Where(m => m.AccountOwnerID == id).ToListAsync();

            foreach(Member member in members) {
                member.CheckIns = await _context.CheckIns.Where(c => c.MemberID == member.ID).ToListAsync();
            }

            return Json(members);

        }

        [HttpPost]
        [Route("log")]
        public async Task<IActionResult> LogIn([FromBody] LogInViewModel cred) {

            //Grabs the user from the database
            var member = await _context.Members
                .Where(m => m.UserName == cred.UserName.ToLower())
                .SingleOrDefaultAsync();

            //Checks to ensure the user actually exists.  Returns false if not.
            if (member == null)
            {
                return Json(false);
            }

            //Encodes the password
            var bytes = Encoding.UTF8.GetBytes(cred.Password);
            var md5 = MD5.Create();

            //Tests the two hashed passwords for equality.  Returns false if not.
            if (member.Password != BitConverter.ToString(md5.ComputeHash(bytes)).Replace("-", String.Empty))
            {
                return Json(false);
            }

            //Gets the list of meetings associated with that account
            var meetings = await _context.CheckIns
                .Where(m => m.MemberID == member.ID)
                .ToListAsync();

            //Sets the check in list to that list of checkings
            member.CheckIns = meetings;
            //Nullifies the password
            member.Password = null;

            //If all is successful, returns the user as json object.
            return Json(member);

        }

        //For Deleting a member
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteMember(int id) {

            //Grabs the member
            var member = await _context.Members
                .Where(m => m.ID == id)
                .FirstOrDefaultAsync();

            //If member doesn't exist, return false.
            if (member == null) {
                return Json(false);
            }

            //Remove the member
            _context.Members.Remove(member);
            //Save the changes
            await _context.SaveChangesAsync();
            //Return former member's username
            return Json(member.UserName);
        
        }

    }
}
