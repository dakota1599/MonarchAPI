using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonarchAPI.Models
{
    public class Meeting
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public int OwnerID { get; set; }
        public string Owner { get; set; }
    }
}
