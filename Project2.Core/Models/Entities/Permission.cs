using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Core.Models.Entities
{
    public class Permission
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public IList<Guest> Guests { get; set; }
    }
}
