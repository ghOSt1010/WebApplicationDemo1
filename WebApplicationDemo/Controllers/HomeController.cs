using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using WebApplicationDemo.Models;
using System.Data.Entity;

namespace WebApplicationDemo.Controllers
{
    public class HomeController : Controller
    {
        [HttpGet]
        public async Task<ActionResult> Index()
        {
            DBContextFull db = new DBContextFull();

            return View(await db.EmployeesFulls.ToListAsync());

        }
        /*
        [HttpPost]
        public string Index(string dateString)
        {
            return dateString;
        }
        */


        [HttpPost]

        public async Task<ActionResult> Index(string id )
        {
            DBContextFull db = new DBContextFull();

            List<EmployeesFull> employees;

            if (string.IsNullOrEmpty(id))
            {
                 employees = await db.EmployeesFulls.ToListAsync();             }
            else
            {
                employees = await db.EmployeesFulls.Where(x => x.FullName.StartsWith(id)).ToListAsync();
            }


            return View( employees);

        }
        [HttpPost]
        public ActionResult MyAction(string valueINeed)
        {
            DateTime startDate = DateTime.ParseExact(valueINeed, "yyyy-MM-dd ,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);
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
        /*
        [HttpPost]
        public ActionResult Index(DateTime? startDate = null)
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
        */
        [HttpPost]
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