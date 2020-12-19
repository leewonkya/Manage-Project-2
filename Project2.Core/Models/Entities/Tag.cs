using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Core.Models.Entities
{
    public class Tag
    {
        public int id { get; set; }
        public string name { get; set; }
        public IList<Project> Projects { get; set; }
    }
}
