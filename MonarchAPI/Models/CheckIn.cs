using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonarchAPI.Models
{
    public class CheckIn
    {
        [Key]
        public int ID { get; set; }
        public string MeetingName { get; set; }
        public int MeetingID { get; set; }
        public int MemberID { get; set; }
        public string MemberName { get; set; }
        public Boolean CheckedIn { get; set; }
    }
}
