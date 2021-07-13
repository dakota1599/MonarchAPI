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

        //Gets a meeting by its id.
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IActionResult> GetMeeting(int id) {

            //Grabs the meeting from the table.
            var meeting = await _context.Meetings.Where(m => m.ID == id).SingleOrDefaultAsync();

            //If meeting is not null...
            if (meeting != null) {
                //...get the checkins associated with the meeting.
                var checkins = await _context.CheckIns.Where(c => c.MeetingID == id).OrderBy(c => c.MemberName).ToListAsync();

                //Set the meetings checkins to the checkin list.
                meeting.CheckIns = checkins;

                //Return a json of the meeting.
                return Json(meeting);
            }
            //Return false if meeting is null.
            return Json(false);

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

        //Edits a record in the table.
        [HttpPut]
        [Route("")]
        public async Task<IActionResult> EditMeeting([FromBody] Meeting meeting) {

            //If the incoming meeting is not null...
            if (meeting != null)
            {
                //...grab the meeting from the table that it is associated with.
                var meetingToChange = await _context.Meetings.Where(m => m.ID == meeting.ID).SingleOrDefaultAsync();
                //If that meeting is not null...
                if (meetingToChange != null)
                {
                    //...set its name to the name of the incoming meeting.
                    meetingToChange.Name = meeting.Name;
                    //Await the saved changes
                    await _context.SaveChangesAsync();
                    //Return true.
                    return Json(true);
                }
                //Return false if the database does not contain that meeting.
                return Json(false);
            }
            //Return false if the incoming meeting is empty.
            return Json(false);

        
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
            if (checkins.Count() != 0)
            {
                _context.CheckIns.RemoveRange(checkins);
            }

            //Save changes
            await _context.SaveChangesAsync();

            //Return true
            return Json(true);

        }

        [HttpGet]
        [Route("org/{id:int}")]
        public async Task<IActionResult> GetOrganizationMeetings(int id) {

            var meetings = await _context.Meetings.Where(m => m.OrgID == id).OrderBy(m => m.Name).ToListAsync();

            return Json(meetings);
        
        }
    }
}
