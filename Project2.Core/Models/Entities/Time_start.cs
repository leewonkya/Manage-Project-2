using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Core.Models.Entities
{
    public class Time_start
    {
        public int id { get; set; }
        public string name { get; set; }
        public DateTime start_at { get; set; }
        public DateTime end_at { get; set; }
        public int numberProject { get; set; }
        public int numberProjectInTeacher { get; set; }
        public bool isFinish { get; set; }

        public IList<Project> Projects { get; set; }
    }
}
