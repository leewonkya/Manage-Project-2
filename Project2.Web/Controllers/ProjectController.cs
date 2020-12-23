using Project2.Core.Interfaces.IServices;
using Project2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Web.Mvc;
using Project2.Web.Models;
using Project2.Core.Models.Entities;
using Project2.Web.Common;

namespace Project2.Web.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        private readonly IProjectService projectService;
        private readonly ITagService tagService;
        private readonly ITimeStartService timeStartService;
        private readonly IGuestService guestService;
        private readonly IReportService reportService;
        private IDataContext dataContext;

        public ProjectController(IProjectService projectService, IDataContext dataContext, ITagService tagService, ITimeStartService timeStartService, IGuestService guestService, IReportService reportService)
        {
            this.projectService = projectService;
            this.dataContext = dataContext;
            this.tagService = tagService;
            this.timeStartService = timeStartService;
            this.guestService = guestService;
            this.reportService = reportService;
        }

        public ActionResult Index()
        {
           
            var listTag = tagService.GetTags().ToList();
            
            ViewBag.listTag = new SelectList(listTag, "id", "name");
            
            return View();
        }

        public ActionResult projectPartialView(string key)
        {
            var userLogin = (UserLogin)Session["userId"];
            var model = projectService.getListProjectById(userLogin.id, key);
            return PartialView("projectPartialView", model);
        }

        public ActionResult sidebarPartialView()
        {
            var userLogin = (UserLogin)Session["userId"];
            var guest = guestService.GetGuestById(userLogin.id);
            ViewBag.guest = guest;
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult addProject(ProjectViewModel model)
        {
            var time = timeStartService.getTime();
            var obj = (UserLogin)Session["userId"];
            var teacher = guestService.GetGuestById(obj.id);
            var cProjectTeach = projectService.getCountProjectByIdTeach(obj.id);
            bool check = projectService.getNameProject(model.name);
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.id == 0)
                    {
                        if(cProjectTeach > 8)
                        {
                            ViewBag.message = "Thêm lỗi, số lượng đề tài tối đa là 8";
                            return RedirectToAction("projectPartialView", "Project");
                        }
                        if(check == false)
                        {
                            return RedirectToAction("projectPartialView", "Project");
                        }
                        else
                        {
                            var data = new Project();
                            data.name = model.name;
                            data.require = model.require;
                            data.time_Start = time;
                            data.GuestTeacher = teacher;
                            dataContext.Projects.Add(data);
                            dataContext.SaveChanges();

                            var projectId = data.id;
                            var newProject = projectService.getProjectById(projectId);
                            data.Tags = new List<Tag>();
                            foreach (var items in model.Tags)
                            {
                                var tag = dataContext.Tags.Include(x => x.Projects).Where(x => x.id.Equals(items)).SingleOrDefault();

                                var i = tag.Projects;
                                if (tag != null)
                                {
                                    data.Tags.Add(tag);
                                }
                            }
                            dataContext.SaveChanges();
                            return RedirectToAction("projectPartialView", "Project");
                        }                        
                    }
                }
                catch (Exception e) {
                    throw e;
                }
            }
            return RedirectToAction("Index", "Project");
        }

        
        public ActionResult removeProject(int id)
        {
            var data = dataContext.Projects.Find(id);
            dataContext.Projects.Remove(data);
            dataContext.SaveChanges();
            return RedirectToAction("projectPartialView", "Project");
        }

        public ActionResult loadReport(int id)
        {
            var project = projectService.getProjectById(id);
            ViewBag.name = project.name;

            ViewBag.id = project.id;
            return View();
        }

        [HttpGet]
        public PartialViewResult reportPartialViewInTeacher(int id)
        {
            var model = reportService.GetReports(id);
            return PartialView("reportPartialViewInTeacher", model);
        }

        [HttpGet]
        public ActionResult changeStatus(int id)
        {
            if(id > 0)
            {
                var model = reportService.getReportById(id);
                model.isSeen = true;
                dataContext.SaveChanges();
                return PartialView("reportPartialViewInTeacher", model);
            }
            return View("loadReport", "Project");
        }
    }
}