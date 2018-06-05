using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JquerySolution.Models;

namespace JquerySolution.Controllers
{
    public class JQueryTestController : Controller
    {
        public ModelServices Ms = new ModelServices();
        // GET: JQueryTest
        public ActionResult Index()
        {
            ViewBag.EmployeeList = Ms.GetEmployee();
            ViewBag.CountryList = Ms.GetCountry();
            return View();
        }
    }
}