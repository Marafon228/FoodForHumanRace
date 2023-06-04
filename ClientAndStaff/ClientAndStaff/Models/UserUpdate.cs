using System;
using System.Collections.Generic;
using System.Text;

namespace ClientAndStaff.Models
{
    public class UserUpdate
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string FirsName { get; set; }
        public string MidleName { get; set; }
        public string LastName { get; set; }
        public string Adress { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int IdRole { get; set; }
    }
}
