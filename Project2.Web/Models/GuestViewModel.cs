using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2.Web.Models
{
    public class GuestViewModel
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Full_name { get; set; }
        public string Path { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }
    }
}