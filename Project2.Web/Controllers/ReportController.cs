using Project2.Core.Interfaces;
using Project2.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project2.Web.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        private readonly IReportService reportService;

        private IDataContext dataContext;

        public ReportController(IReportService reportService, IDataContext dataContext)
        {
            this.reportService = reportService;
            this.dataContext = dataContext;
        }

        public ActionResult Index()
        {
            return View();
        }
    }
}