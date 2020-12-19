using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Core.Models.Entities
{
    public class Guest
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Full_name { get; set; }
        public string Path { get; set; }
        public DateTime? Birthday { get; set; }
        public string Email { get; set; }

        public Permission Permission { get; set; }


        public IList<Project> ProjectsStudent { get; set; }
        public IList<Project> ProjectsTeacher { get; set; }
    }
}
