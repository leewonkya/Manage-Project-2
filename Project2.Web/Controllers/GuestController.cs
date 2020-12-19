using Project2.Core.Interfaces;
using Project2.Core.Interfaces.IServices;
using Project2.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;

namespace Project2.Web.Controllers
{
    public class GuestController : Controller
    {
        // GET: Guest
        private readonly IGuestService guestService;

        private IDataContext dataContext;

        public GuestController(IDataContext dataContext, IGuestService guestService)
        {
            this.dataContext = dataContext;
            this.guestService = guestService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = guestService.GetGuestById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Guest model, HttpPostedFileBase file)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if(model.Id > 0)
                    {
                        if(file != null && file.ContentLength > 0)
                        {
                            try
                            {
                                string path = Path.Combine(Server.MapPath("~/Content/asset/public/Image"),
                                       Path.GetFileName(file.FileName));
                                file.SaveAs(path);
                            }
                            catch (Exception) { }
                        }
                        var data = guestService.GetGuestById(model.Id);
                        data.Username = model.Username;
                        data.Password = model.Password;
                        data.Full_name = model.Full_name;
                        data.Birthday = model.Birthday;
                        data.Path = model.Path;
                        data.Email = model.Email;                        
                        dataContext.SaveChanges();
                    }
                }
            }
            catch (Exception) { }
            return RedirectToAction("Index", "Guest");
        }

        public ActionResult Remove(int id)
        {
            if(id > 0)
            {
                var data = dataContext.Guests.Find(id);
                if(data.Id > 0)
                {
                    dataContext.Guests.Remove(data);
                    dataContext.SaveChanges();
                }
            }
            return RedirectToAction("Index", "Guest");
        }

        public ActionResult guestPartialView()
        {
            var model = guestService.getListGuests().ToList();

            return PartialView("guestPartialView", model);
        }

    }
}