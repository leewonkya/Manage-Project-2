using Project2.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2.Web.Models
{
    public class ProjectModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public bool isActive { get; set; }
        public string require { get; set; }
        public Tag Tag { get; set; }
        public Guest GuestStudent { get; set; }
        public Guest GuestTeacher { get; set; }
        public Time_start time_Start { get; set; }
    }
}