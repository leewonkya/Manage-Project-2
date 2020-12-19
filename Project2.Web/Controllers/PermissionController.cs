using Project2.Core.Interfaces;
using Project2.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project2.Web.Controllers
{
    public class PermissionController : Controller
    {
        // GET: Permission
        private readonly IPermissionService permissionService;

        private IDataContext dataContext;

        public PermissionController(IPermissionService permissionService, IDataContext dataContext)
        {
            this.permissionService = permissionService;
            this.dataContext = dataContext;
        }
        public ActionResult Index()
        {
            return View();
        }
    }
}