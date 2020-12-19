using Project2.Core.Interfaces.IServices;
using Project2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Project2.Core.Models.Entities;

namespace Project2.Core.Services
{
    public class PermissionService : IPermissionService
    {
        private IDataContext context;

        public PermissionService(IDataContext context)
        {
            this.context = context;
        }

        public string getPermissionNameById(int id) {         
            return context.Permissions.Where(x => x.Id == id).Select(x => x.Title).SingleOrDefault();
        }
    }
}
