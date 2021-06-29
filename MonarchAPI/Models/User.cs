using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonarchAPI.Models
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Name{ get; set; }
        [MaxLength(100)]
        public string UserName { get; set; }
        public string Password { get; set; }
        public Boolean CheckedIn { get; set; }
    }
}
