using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Core.Models.Entities
{
    public class Project
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }
        public bool isActive { get; set; }
        public string require { get; set; }
        
        public IList<Tag> Tags { get; set; }
        public IList<Report> Reports { get; set; }

        public Guest GuestStudent { get; set; }
        public Guest GuestTeacher { get; set; }
        public Time_start time_Start { get; set; }
    }
}
