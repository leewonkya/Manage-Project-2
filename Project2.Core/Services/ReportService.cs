using Project2.Core.Interfaces;
using Project2.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project2.Core.Services
{
    public class ReportService : IReportService
    {
        private IDataContext context;

        public ReportService(IDataContext context)
        {
            this.context = context;
        }
    }
}
