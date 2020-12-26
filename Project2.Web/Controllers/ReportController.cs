using Project2.Core.Interfaces;
using Project2.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Project2.Web.Common;
using System.Web.Mvc;
using Project2.Core.Models.Entities;

namespace Project2.Web.Controllers
{
    public class ReportController : Controller
    {
        // GET: Report
        private readonly IReportService reportService;
        private readonly ITimeStartService timeStartService;
        private readonly IProjectService projectService;
        private IDataContext dataContext;

        public ReportController(IReportService reportService, IDataContext dataContext, ITimeStartService timeStartService, IProjectService projectService)
        {
            this.reportService = reportService;
            this.dataContext = dataContext;
            this.timeStartService = timeStartService;
            this.projectService = projectService;
        }

        public ActionResult Index()
        {
            var userLogin = (UserLogin)Session["userId"];
            var model = projectService.getListProjectByStudentId(userLogin.id).ToList();
            var project = projectService.getProjectByStudentId(userLogin.id);
            ViewBag.projectId = project.id;
            return View(model);
        }

        [HttpGet]
        public PartialViewResult reportPartialView(int id)
        {
            Project model = null;
            if(id != 0)
            {
                model = projectService.getProjectById(id);
                return PartialView("reportPartialView", model);
            }
            return PartialView("reportPartialView", model);            
        }
        
        [HttpPost]
        [ValidateInput(false)]
        public ActionResult addReport(Report model, int projectId)
        {
            var project = projectService.getProjectById(projectId);
            if(model != null)
            {
                var data = new Report();
                data.title = model.title;
                data.create_at = DateTime.Now;
                data.isSeen = false;
                data.content = model.content;
                data.Project = project;
                dataContext.Reports.Add(data);
                dataContext.SaveChanges();
                return RedirectToAction("Index", "Report");
            }
            return RedirectToAction("Index", "Report");
        }
        
        public ActionResult removeReport(int id)
        {            
            var model = reportService.getReportById(id);
            var project = projectService.getProjectById(model.Project.id);
            dataContext.Reports.Remove(model);
            dataContext.SaveChanges();
            return RedirectToAction("Index", "Report");
        }
    }
}