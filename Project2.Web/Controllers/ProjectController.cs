using Project2.Core.Interfaces.IServices;
using Project2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project2.Web.Controllers
{
    public class ProjectController : Controller
    {
        // GET: Project
        private readonly IProjectService projectService;
        private readonly ITagService tagService;

        private IDataContext dataContext;

        public ProjectController(IProjectService projectService, IDataContext dataContext, ITagService tagService)
        {
            this.projectService = projectService;
            this.dataContext = dataContext;
            this.tagService = tagService;
        }

        public ActionResult Index()
        {
            var listTag = tagService.GetTags().ToList();
            ViewBag.listTag = new SelectList(listTag, "id", "name");
            return View();
        }

        public ActionResult projectPartialView()
        {
            var model = projectService.GetProjects();
            return PartialView("projectPartialView", model);
        }
    }
}