using Project2.Core.Interfaces;
using Project2.Core.Interfaces.IServices;
using Project2.Core.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using OfficeOpenXml;
using Excel = Microsoft.Office.Interop.Excel;

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
        public ActionResult Add(int id)
        {
            ViewBag.idPer = id;
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var model = guestService.GetGuestById(id);
            return View(model);
        }

        [HttpPost]
        public ActionResult Edit(Guest model, HttpPostedFileBase file, int idPer)
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
                    else
                    {
                        var obj = new Guest();
                        var objPer = permissionService.getPerById(idPer);
                        if (file != null)
                        {
                            try
                            {

                                string path = Path.Combine(Server.MapPath("~/Content/asset/public/Image/"),
                                       Path.GetFileName(file.FileName));
                                file.SaveAs(path);
                                obj.Path = file.FileName;
                            }
                            catch (Exception) { }
                        }
                        else
                        {
                            obj.Path = model.Path;
                        }

                        obj.Username = model.Username;
                        obj.Password = model.Password;
                        obj.Full_name = model.Full_name;
                        obj.Birthday = model.Birthday;
                        obj.Email = model.Email;
                        if(idPer == 2)
                        {
                            obj.Permission = objPer;
                        }
                        else if(idPer == 3)
                        {
                            obj.Permission = objPer;
                        }
                        dataContext.Guests.Add(obj);
                        dataContext.SaveChanges();
                        return RedirectToAction("loadTable", "Guest", new { id = obj.Permission.Id });
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

        [HttpGet]
        public ActionResult loadTable(int id)
        {
            string title = permissionService.getPermissionNameById(id);
            ViewBag.title = title;
            ViewBag.id = id;
            return View();
        }

        public PartialViewResult guestPartialView(int id, string search)
        {
            List<Guest> model = null;
            if (!string.IsNullOrEmpty(search))
            {
                model = guestService.getlistByNameAndId(id, search).ToList();
                return PartialView("guestPartialView", model);
            }
            else
            {
                model = guestService.getListGuestByIdPermission(id).ToList();
                return PartialView("guestPartialView", model);
            }
        }

        // export Excel

        public ActionResult downExcel(int id)
        {
            var listGuest = guestService.getListGuestByIdPermission(id).ToList();

            using (ExcelPackage epk = new ExcelPackage())
            {
                ExcelWorksheet ws = epk.Workbook.Worksheets.Add("Guest");
                ws.Cells["A1"].Value = "ID";
                ws.Cells["B1"].Value = "Username";
                ws.Cells["C1"].Value = "Password";
                ws.Cells["D1"].Value = "Fullname";
                ws.Cells["E1"].Value = "Path";
                ws.Cells["F1"].Value = "Birthday";
                ws.Cells["G1"].Value = "Email";
                ws.Cells["H1"].Value = "Thuộc nhóm";
                int row = 2;
                foreach(var item in listGuest)
                {
                    ws.Cells[string.Format("A{0}", row)].Value = item.Id.ToString();
                    ws.Cells[string.Format("B{0}", row)].Value = item.Username;
                    ws.Cells[string.Format("C{0}", row)].Value = item.Password;
                    ws.Cells[string.Format("D{0}", row)].Value = item.Full_name;
                    ws.Cells[string.Format("E{0}", row)].Value = item.Path;
                    ws.Cells[string.Format("F{0}", row)].Value = item.Birthday.ToString();
                    ws.Cells[string.Format("G{0}", row)].Value = item.Email;
                    ws.Cells[string.Format("H{0}", row)].Value = item.Permission.Title;
                    row++;
                }

                ws.Cells["A:AZ"].AutoFitColumns();
                Response.Clear();
                Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                Response.AddHeader("content-disposition", "attachment: filename= "+"Report.xlsx");
                Response.BinaryWrite(epk.GetAsByteArray());
                Response.End();
            }

            return RedirectToAction("loadTable", "Guest");
        }

        [HttpPost]
        public ActionResult importExcel(int id, HttpPostedFileBase files)
        {
            var permission = permissionService.getPerById(id);
            
            if(files.FileName.EndsWith("xls") || files.FileName.EndsWith("xlsx"))
            {
                string path = Path.Combine(Server.MapPath("~/Content/asset/public/uploads/"),
                                       Path.GetFileName(files.FileName));
                string extension = Path.GetExtension(path);
                if (System.IO.File.Exists(path))
                    System.IO.File.Replace(path, "temp" + extension, "backup" + extension);
                files.SaveAs(path);

                Excel.Application application = new Excel.Application();
                Excel.Workbook workbook = application.Workbooks.Open(path);
                Excel.Worksheet worksheet = workbook.ActiveSheet;
                Excel.Range range = worksheet.UsedRange;
                for (int row = 2; row < range.Rows.Count; row++)
                {
                    var guest = new Guest();
                    guest.Username = ((Excel.Range)range.Cells[row, 2]).Text;
                    guest.Password = ((Excel.Range)range.Cells[row, 3]).Text;
                    guest.Full_name = ((Excel.Range)range.Cells[row, 4]).Text;
                    guest.Path = ((Excel.Range)range.Cells[row, 5]).Text;
                    guest.Birthday = DateTime.ParseExact(((Excel.Range)range.Cells[row, 6]).Text, "dd/MM/yyyy", null);
                    guest.Email = ((Excel.Range)range.Cells[row, 7]).Text;
                    guest.Permission = permission;
                    dataContext.Guests.Add(guest);
                }
                dataContext.SaveChanges();
                workbook.Close(path);
                //int startCol = 1;
                //int startRow = 2;
                //var package = new ExcelPackage();
                //ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
                //Guest obj = new Guest();
                //object data = null;
                //do
                //{
                //    data = worksheet.Cells[startRow, startCol].Value;
                //    if (data != null)
                //    {
                //        string username = worksheet.Cells[startRow, startCol + 1].Value.ToString();
                //        string password = worksheet.Cells[startRow, startCol + 2].Value.ToString();
                //        string fullname = worksheet.Cells[startRow, startCol + 3].Value.ToString();
                //        string path = worksheet.Cells[startRow, startCol + 4].Value.ToString();
                //        string tempBirthday = worksheet.Cells[startRow, startCol + 5].Value.ToString();
                //        DateTime? birthday = DateTime.Parse(tempBirthday);
                //        string email = worksheet.Cells[startRow, startCol + 6].Value.ToString();
                //        //string tempIdPer = worksheet.Cells[startRow, startCol + 7].Value.ToString();
                //        //int idPer = int.Parse(tempIdPer);
                //        if (!string.IsNullOrEmpty(username)
                //            && !string.IsNullOrEmpty(username)
                //            && !string.IsNullOrEmpty(password)
                //            && !string.IsNullOrEmpty(fullname)
                //            && !string.IsNullOrEmpty(path)
                //            && !string.IsNullOrEmpty(tempBirthday)
                //            && !string.IsNullOrEmpty(email))
                //        {
                //            obj.Username = username;
                //            obj.Password = password;
                //            obj.Full_name = fullname;
                //            obj.Path = path;
                //            obj.Birthday = birthday;
                //            obj.Email = email;
                //            obj.Permission = permission;
                //            dataContext.Guests.Add(obj);
                //            dataContext.SaveChanges();
                //        }
                //    }
                //} while (data != null);
            }

            return RedirectToAction("loadTable", "Guest", new { id = permission.Id });
        }
    }
}