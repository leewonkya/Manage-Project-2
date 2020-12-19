using Project2.Core.Interfaces.IServices;
using Project2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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
    }
}