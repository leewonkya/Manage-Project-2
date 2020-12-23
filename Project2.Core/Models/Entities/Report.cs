using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Core.Models.Entities
{
    public class Report
    {
        public int id { get; set; }
        public string title { get; set; }
        public string content { get; set; }
        public bool isSeen { get; set; }
        public DateTime create_at { get; set; }

        public Project Project { get; set; }
    }
}
