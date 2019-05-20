using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplicationDemo.Models;

namespace WebApplicationDemo.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            DBContextFull db = new DBContextFull();
            //SampleDBContext db = new SampleDBContext();

            return View(db.EmployeesFulls);

        }
       
        [HttpPost]
        public ActionResult Index(string searchTerm)
        {
            DBContextFull db = new DBContextFull();

            List<EmployeesFull> employees;

            if (string.IsNullOrEmpty(searchTerm))
            {
                employees = db.EmployeesFulls.ToList();             }
            else
            {
                employees = db.EmployeesFulls.Where(x => x.FullName.StartsWith(searchTerm)).ToList();
            }

            return View(employees);

        }
        
        public ActionResult SelectDate(DateTime startDate)
        {
            DBContextFull dbf = new DBContextFull();

            List<EmployeesFull> employees;

            if (startDate != null)
            {
                employees = dbf.EmployeesFulls.Where(l => l.JoiningDate <= startDate).ToList();
            }
            else
            {
                employees = dbf.EmployeesFulls.ToList();
            }
            return View(employees);

        }
        public JsonResult Autocomplete(string term)

        {
            DBContextFull dbf = new DBContextFull();

            List<string> Employees = dbf
                .EmployeesFulls
                .Where(p => p.FullName.ToLower().Contains(term.ToLower()))
                .Select(p => p.FullName)
                .ToList();
            return Json(Employees, JsonRequestBehavior.AllowGet);

        }




    }
}