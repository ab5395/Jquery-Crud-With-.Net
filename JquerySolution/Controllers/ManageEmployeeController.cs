using System;
using System.IO;
using System.Web.Mvc;
using JquerySolution.Models;
using static System.IO.Directory;

namespace JquerySolution.Controllers
{
    public class ManageEmployeeController : Controller
    {
        public ModelServices Ms = new ModelServices();

        [HttpGet]
        public ActionResult EmployeeList()
        {
            ViewBag.EmployeeList = Ms.GetEmployee();
            ViewBag.CountryList = Ms.GetCountry();
            return View();
        }

        public JsonResult GetEmployeeList()
        {
            ViewBag.EmployeeList = Ms.GetEmployee();
            return Json("true", JsonRequestBehavior.AllowGet);
        }

        public PartialViewResult AddEmployeerPartialView()
        {
            return PartialView("_AddEmployee");
        }

        public PartialViewResult EditEmployeerPartialView()
        {
            return PartialView("_EditEmployee");
        }

        public JsonResult InsertEmployee(string city, string name, string department, string mobile)
        {
            Employee employee = new Employee()
            {
                Name = name,
                Department = department,
                CityId = int.Parse(city),
                Mobile = long.Parse(mobile)
            };
            Ms.AddEmployee(employee);
            ViewBag.EmployeeList = Ms.GetEmployee();
            var countrylist = Ms.GetCountry();
            return Json(new SelectList(countrylist.ToArray(), "CountryId", "Country1", countrylist), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UpdateEmployee(string city, string name, string department, string mobile,string employee)
        {
            Employee emp = new Employee();
            emp.EmpId = int.Parse(employee);
            emp.Name = name;
            emp.Department = department;
            emp.Mobile = long.Parse(mobile);
            emp.CityId = int.Parse(city);
            Ms.UpdateEmployee(emp);
            ViewBag.EmployeeList = Ms.GetEmployee();
            return Json(new {success=true}, JsonRequestBehavior.AllowGet);
        }

        public JsonResult EditEmployee(string id)
        {
            Employee emp = Ms.GetEmployeeDetail(int.Parse(id));
            return Json(new
            {
                empid = emp.EmpId.ToString(),
                EName = emp.Name,
                DName = emp.Department,
                Mobile = emp.Mobile.ToString(),
                CityId = emp.CityId.ToString(),
                CName = emp.City.City1,
                StateId = emp.City.StateId.ToString(),
                SName = emp.City.State.State1,
                CoName=emp.City.State.Country.Country1,
                CountryId=emp.City.State.CountryId.ToString()
            }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteEmployee(string id)
        {
            Employee emp = Ms.GetEmployeeDetail(int.Parse(id));
            Ms.DeleteEmployee(emp);
            ViewBag.EmployeeList = Ms.GetEmployee();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult MultiDeleteEmployee(string id)
        {
            var eid = id.Split(',');
            foreach (var data in eid)
            {
                Employee emp = Ms.GetEmployeeDetail(int.Parse(data));
                Ms.DeleteEmployee(emp);
            }
            ViewBag.EmployeeList = Ms.GetEmployee();
            return Json(new { success = true }, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult InsertCountry(string countryname)
        {
            Country country = new Country()
            {
                Country1 = countryname
            };
            int id = Ms.AddCountry(country);
            ViewBag.CountryList = Ms.GetCountry();
            var countrylist = Ms.GetCountry();
            return Json(new SelectList(countrylist.ToArray(), "CountryId", "Country1", countrylist), JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindCountry()
        {
            var country = Ms.GetCountry();
            return Json(new SelectList(country.ToArray(), "CountryId", "Country1"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindCityList(int id)
        {
            var city = Ms.GetCityByStateId(id);
            return Json(new SelectList(city.ToArray(), "CityId", "City1"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult BindStateList(int id)
        {
            var state = Ms.GetStateByCountryId(id);
            return Json(new SelectList(state.ToArray(), "StateId", "State1"), JsonRequestBehavior.AllowGet);
        }

        public JsonResult UploadFiles(int id)
        {
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
                        Image image=new Image()
                        {
                            EmployeeId = id,
                            Image1 = changename
                        };
                        Ms.AddImage(image);
                    }
                }
                return Json(new { success = true }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = false }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult ImageListByEmpId(int id)
        {
            var imagelist = Ms.GetImagesByEid(id);
            return Json(new SelectList(imagelist.ToArray(), "EmployeeId", "Image1"), JsonRequestBehavior.AllowGet);
        }
    }
}