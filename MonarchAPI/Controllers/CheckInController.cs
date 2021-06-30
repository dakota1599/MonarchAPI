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
    public class CheckInController : Controller
    {
        protected MonarchContext _context { get; set; }

        public CheckInController(MonarchContext context) {
            this._context = context;
        }

        //Adds a checkin for a member
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateCheckIn([FromBody] CheckIn check) {

            //Checks to see if the checkin already exists
            var val = await _context.CheckIns
                .Where(c => c.MeetingID == check.MeetingID)
                .Where(c => c.MemberID == check.MemberID)
                .FirstOrDefaultAsync();

            //If so, return false
            if (val != null) {
                return Json(false);
            }

            //If not, add to checkins
            _context.CheckIns.Add(check);

            //Save changes
            await _context.SaveChangesAsync();

            //Return true
            return Json(true);
        
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteCheckIn(int id) {

            var check = await _context.CheckIns
                .Where(c => c.ID == id)
                .FirstOrDefaultAsync();

            if (check == null) {
                return Json(false);
            }

            _context.CheckIns.Remove(check);

            await _context.SaveChangesAsync();

            return Json(true);
        
        }
    }
}
