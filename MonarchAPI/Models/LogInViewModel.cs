using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MonarchAPI.Models
{
    public class LogInViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
