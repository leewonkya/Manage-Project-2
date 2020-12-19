using Project2.Core.Interfaces;
using Project2.Core.Interfaces.IServices;
using Project2.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Core.Services
{
    public class TimeStartService : ITimeStartService
    {
        private IDataContext context;

        public TimeStartService(IDataContext context)
        {
            this.context = context;
        }

        public List<Time_start> getTimeStart()
        {
            return context.Time_Starts.OrderByDescending(x => x.isFinish == false).ToList();
        }
    }
}
