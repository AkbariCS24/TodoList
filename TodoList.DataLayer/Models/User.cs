using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TodoList.DataLayer.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "UserName Is Required")]
        [MaxLength(50)]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "Password Is Required")]
        [MaxLength(50)]
        public string Password { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
}
