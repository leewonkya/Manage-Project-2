using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project2.Core.Interfaces;
using Project2.Core.Interfaces.IServices;
using Project2.Web.Common;

namespace Project2.Web.Controllers
{
    public class StudentController : Controller
    {
        // GET: Student
        private readonly IGuestService guestService;
        private readonly IProjectService projectService;
        private readonly ITagService tagService;
        private IDataContext dataContext;

        public StudentController(IGuestService guestService, IDataContext dataContext, IProjectService projectService, ITagService tagService)
        {
            this.guestService = guestService;
            this.dataContext = dataContext;
            this.projectService = projectService;
            this.tagService = tagService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult studentPartialView(string search)
        {
            var model = projectService.getListProject(search);
            return PartialView("studentPartialView", model);
        }

        public ActionResult addOrSwap(int id)
        {
            var userLogin = (UserLogin)Session["userId"]; // lay id user khi Login
            var guest = guestService.GetGuestById(userLogin.id); // lay ra doi tuong sinh vien

            var oldProject = projectService.getProjectByStudentId(userLogin.id);
            var newProject = projectService.getProjectById(id); // lay ra doi tuong project moi

            bool response = false;
            if(oldProject == null) // neu student va project unActive 
            {
                newProject.GuestStudent = guest; // set student = doi tuong sinh vien
                newProject.isActive = true; // set Active
                dataContext.SaveChanges();
                response = true;
                return RedirectToAction("studentPartialView", "Student", response);
            }
            else
            {
                oldProject.GuestStudent = null;
                oldProject.isActive = false;
                newProject.GuestStudent = guest; // set student = doi tuong sinh vien
                newProject.isActive = true; // set Active
                dataContext.SaveChanges();
                response = true;
                return RedirectToAction("studentPartialView", "Student", response);
            }
        }
    }
}