using Project2.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project2.Web.Models
{
    public class ProjectViewModel
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime create_at { get; set; }
        public DateTime update_at { get; set; }
        public bool isActive { get; set; }
        public string require { get; set; }

        public List<Tag> Tags { get; set; }
        public string studentId { get; set; }
        public string studentName { get; set; }
        public string timeStartId { get; set; }
        public List<Report> Reports { get; set; }
    }
}