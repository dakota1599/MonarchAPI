using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonarchAPI.Models
{
    public class Organization
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
    }
}
