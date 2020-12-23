using Project2.Core.Interfaces;
using Project2.Core.Interfaces.IServices;
using Project2.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
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

        public List<Report> GetReports(int id)
        {
            return context.Reports.Include(x => x.Project).Where(x => x.Project.id == id).ToList();
        }

        public Report getReportById(int id)
        {
            return context.Reports
                .Include(x => x.Project)
                .Include(x => x.Project.GuestStudent)
                .Include(x => x.Project.GuestTeacher)
                .Include(x => x.Project.time_Start)
                .Where(x => x.id == id)
                .SingleOrDefault();
        }
    }
}
