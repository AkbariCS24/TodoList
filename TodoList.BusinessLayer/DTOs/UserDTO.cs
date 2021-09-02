using System;
using System.Collections.Generic;
using System.Text;

namespace TodoList.BusinessLayer.DTOs
{
    public class UserDTO
    {
        public int ID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
