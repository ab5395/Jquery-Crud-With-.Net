using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JquerySolution.Models;
using static System.IO.Directory;

namespace JquerySolution.Controllers
{
    public class TestController : Controller
    {
        public ModelServices Ms = new ModelServices();

        [HttpGet]
        public ActionResult ImageForm()
        {
            ViewBag.EmployeeList = Ms.GetEmployee();
            return View();
        }

        [HttpPost]
        public ActionResult ImageForm(FormCollection fc)
        {  
            for (int i = 0; i < Request.Files.Count; i++)
            {
                var file = Request.Files[i];
                if (file != null)
                {
                    var imagename = file.FileName;
                    var changename = Guid.NewGuid() + imagename;
                    var imageext = Path.GetExtension(imagename);
                    CreateDirectory(Server.MapPath("~/Images/" + fc["txtEmployeeId"]));
                    file.SaveAs(Server.MapPath("~/Images/" + fc["txtEmployeeId"]+"/") + changename);
                }
            }
            ViewBag.EmployeeList = Ms.GetEmployee();
            return View();
        }

        [HttpGet]
        public ActionResult AjaxImageForm()
        {
            ViewBag.EmployeeList = Ms.GetEmployee();
            return View();
        }



        public ActionResult Box()
        {
            return View();

        }



        public JsonResult UploadFiles(int id)
        {
            // Checking no of files injected in Request object  
            if (Request.Files.Count > 0)
            {
                for (int i = 0; i < Request.Files.Count; i++)
                {
                    var file = Request.Files[i];
                    if (file != null)
                    {
                        var imagename = file.FileName;
                        var changename = Guid.NewGuid() + imagename;
                        var imageext = Path.GetExtension(imagename);
                        CreateDirectory(Server.MapPath("~/Images/" + id));
                        file.SaveAs(Server.MapPath("~/Images/" + id + "/") + changename);
                    }
                }
            }
            else
            {
                return Json(new { success = false }, JsonRequestBehavior.AllowGet);
            }
            return Json(new {success=true},JsonRequestBehavior.AllowGet);
        }
    }
}