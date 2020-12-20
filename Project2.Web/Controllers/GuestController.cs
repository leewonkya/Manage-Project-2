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
        private readonly IPermissionService permissionService;
        private IDataContext dataContext;

        public GuestController(IDataContext dataContext, IGuestService guestService, IPermissionService permissionService)
        {
            this.dataContext = dataContext;
            this.guestService = guestService;
            this.permissionService = permissionService;
        }

        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult editProfile(int id)
        {
            var model = guestService.GetGuestById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult editProfile(Guest model, HttpPostedFileBase file)
        {
            string controller = "";
            string view = "";
            try
            {
                var data = guestService.GetGuestById(model.Id);
                int perId = data.Permission.Id;
                
                if(perId == 1)
                {
                    controller = "TimeStart";
                    view = "Index";
                }
                else if(perId == 2){
                    controller = "Project";
                    view = "Index";
                }
                else
                {
                    controller = "Student";
                    view = "Index";
                }
                if (ModelState.IsValid)
                {
                    if (model.Id > 0)
                    {
                        if (file != null)
                        {
                            try
                            {
                                string path = Path.Combine(Server.MapPath("~/Content/asset/public/Image/"),
                                       Path.GetFileName(file.FileName));
                                file.SaveAs(path);
                                data.Path = file.FileName;
                            }
                            catch (Exception) { }
                        }
                        else
                        {
                            data.Path = model.Path;
                        }
                        
                        data.Username = model.Username;
                        data.Password = model.Password;
                        data.Full_name = model.Full_name;
                        data.Birthday = model.Birthday;
                        data.Email = model.Email;
                        dataContext.SaveChanges();
                        return RedirectToAction(view, controller, new { id = data.Permission.Id });
                    }
                }
            }
            catch (Exception) { }
            return RedirectToAction(view, controller);
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
            var data = guestService.GetGuestById(model.Id);
            try
            {
                if (ModelState.IsValid)
                {
                    if(model.Id > 0)
                    {
                        if(file != null)
                        {
                            try
                            {
                                
                                string path = Path.Combine(Server.MapPath("~/Content/asset/public/Image/"),
                                       Path.GetFileName(file.FileName));
                                file.SaveAs(path);
                                data.Path = file.FileName;
                            }
                            catch (Exception) { }
                        }
                        else
                        {
                            data.Path = model.Path;
                        }
                        
                        data.Username = model.Username;
                        data.Password = model.Password;
                        data.Full_name = model.Full_name;
                        data.Birthday = model.Birthday;
                        
                        data.Email = model.Email;                        
                        dataContext.SaveChanges();
                        return RedirectToAction("loadTable", "Guest", new { id = data.Permission.Id });
                    }
                }
            }
            catch (Exception) { }
            return RedirectToAction("Index", "TimeStart");
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
                    return RedirectToAction("loadTable", "Guest", new { id = data.Permission.Id });
                }
            }
            return RedirectToAction("Index", "TimeStart");
        }


        public ActionResult loadTable(int id)
        {
            var model = guestService.getListGuestByIdPermission(id).ToList();
            string title = permissionService.getPermissionNameById(id);
            ViewBag.title = title;
            return View(model);
        }
    }
}