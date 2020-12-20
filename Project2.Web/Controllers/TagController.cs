using Project2.Core.Interfaces.IServices;
using Project2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project2.Core.Models.Entities;
using Project2.Web.Models;

namespace Project2.Web.Controllers
{
    public class TagController : Controller
    {
        // GET: Tag
        private readonly ITagService tagService;

        private IDataContext dataContext;

        public TagController(ITagService tagService, IDataContext dataContext)
        {
            this.tagService = tagService;
            this.dataContext = dataContext;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult addTag(ProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                if(model.id == 0)
                {
                    var data = new Tag();
                    data.name = model.tagName;
                    dataContext.Tags.Add(data);
                    dataContext.SaveChanges();
                    return RedirectToAction("Index", "Project");
                }
            }
            return RedirectToAction("Index", "Project");
        }
    }
}