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
        private IDataContext dataContext;

        public ProjectController(IProjectService projectService, IDataContext dataContext, ITagService tagService, ITimeStartService timeStartService, IGuestService guestService)
        {
            this.projectService = projectService;
            this.dataContext = dataContext;
            this.tagService = tagService;
            this.timeStartService = timeStartService;
            this.guestService = guestService;
        }

        public ActionResult Index()
        {
            var listTag = tagService.GetTags().ToList();
            ViewBag.listTag = new SelectList(listTag, "id", "name");
            return View();
        }

        public ActionResult projectPartialView(string key)
        {
            var model = projectService.getListProject(key);
            return PartialView("projectPartialView", model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult addProject(ProjectViewModel model)
        {
            var time = timeStartService.getTime();
            var obj = (UserLogin)Session["userId"];
            var teacher = guestService.GetGuestById(obj.id);
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.id == 0)
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
                            if(tag != null)
                            {
                                data.Tags.Add(tag);
                            }
                        }
                        dataContext.SaveChanges();
                        return RedirectToAction("Index", "Project");
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
    }
}