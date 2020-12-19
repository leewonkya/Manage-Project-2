using Project2.Core.Interfaces.IServices;
using Project2.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Project2.Core.Models.Entities;

namespace Project2.Web.Controllers
{
    public class TimeStartController : Controller
    {
        // GET: TimeStart
        private readonly ITimeStartService timeStartService;

        private IDataContext dataContext;

        public TimeStartController(ITimeStartService timeStartService, IDataContext dataContext)
        {
            this.timeStartService = timeStartService;
            this.dataContext = dataContext;
        }
        public ActionResult Index()
        {            
            return View();
        }

        [HttpGet]
        public ActionResult TimeStartPartialView()
        {
            var model = timeStartService.getTimeStart();
            return PartialView("TimeStartPartialView", model);
        }

        [HttpPost]
        public ActionResult Add(Time_start model)
        {
            try
            {
                
                if (model.id == 0)
                {
                    Time_start time = new Time_start();
                    time.name = model.name;
                    time.start_at = model.start_at;
                    time.end_at = model.end_at;
                    time.numberProject = model.numberProject;
                    time.numberProjectInTeacher = model.numberProjectInTeacher;
                    dataContext.Time_Starts.Add(time);
                    dataContext.SaveChanges();
                }
            }
            catch (Exception) { }
            return RedirectToAction("Index", "TimeStart");
        }
        [HttpPost]
        public ActionResult Remove(int id)
        {
            try
            {
                var data = dataContext.Time_Starts.Find(id);
                dataContext.Time_Starts.Remove(data);
                dataContext.SaveChanges();
            }
            catch (Exception) { }
            return RedirectToAction("TimeStartPartialView", "TimeStart");
        }

        public ActionResult Block(int id)
        {
            try
            {
                var data = dataContext.Time_Starts.Find(id);
                if(data.id != 0)
                {
                    data.isFinish = true;
                    dataContext.SaveChanges();
                }
            }
            catch (Exception) { }
            return RedirectToAction("TimeStartPartialView", "TimeStart");
        }
    }
}