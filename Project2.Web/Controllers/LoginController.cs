﻿using Project2.Core.Interfaces;
using Project2.Core.Interfaces.IServices;
using Project2.Core.Models.Entities;
using Project2.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project2.Web.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        private readonly IGuestService guestService;

        private IDataContext dataContext;

        public LoginController(IGuestService guestService, IDataContext dataContext)
        {
            this.guestService = guestService;
            this.dataContext = dataContext;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult checkLog(LoginViewModel model)
        {
            var username = model.Username;
            var password = model.Password;

            var id = guestService.getPermissionIdByUsernameAndPassword(username, password);
            //int id = guestService.getPermissionIdById(data.Id);
            if(id == 1)
            {
                return RedirectToAction("Index", "TimeStart", id);
            }
            else if(id == 2)
            {
                return RedirectToAction("Index", "Project", id);
            }
            else if(id == 3)
            {
                return RedirectToAction("Index", "Guest", id);
            }
            return View();
        }
    }
}