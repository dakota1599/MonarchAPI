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
    public class MeetingController : Controller
    {
        protected MonarchContext _context { get; set; }

        public MeetingController(MonarchContext context) {

            this._context = context;

        }

        //Creates a new meeting
        [HttpPost]
        [Route("")]
        public async Task<IActionResult> CreateMeeting([FromBody] Meeting meeting) {

            //If passed meeting is null, return false.
            if (meeting == null) {
                return Json(false);
            }
            //Add meeting to meetings table.
            _context.Meetings.Add(meeting);
            //Save changes
            await _context.SaveChangesAsync();
            //Return true.
            return Json(true);
        
        }

        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IActionResult> DeleteMeeting(int id) {

            //Retrieves the meeting from the database
            var meeting = await _context.Meetings
                .Where(m => m.ID == id)
                .FirstOrDefaultAsync();

            //If meeting is null, return false
            if (meeting == null) {
                return Json(false);
            }

            //Remove the meeting
            _context.Meetings.Remove(meeting);

            //Get all checkins associated with that meeting
            var checkins = await _context.CheckIns
                .Where(c => c.MeetingID == id)
                .ToListAsync();

            //Remove all those checkins
            _context.CheckIns.RemoveRange(checkins);

            //Save changes
            await _context.SaveChangesAsync();

            //Return true
            return Json(true);

        }

        [HttpGet]
        [Route("org/{id:int}")]
        public async Task<IActionResult> GetOrganizationMeetings(int id) {

            var meetings = await _context.Meetings.Where(m => m.OrgID == id).ToListAsync();

            return Json(meetings);
        
        }
    }
}
