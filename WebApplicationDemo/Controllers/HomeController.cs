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
        readonly DBContextFull db = new DBContextFull();

        public async Task<ActionResult> Index()
        {
            //DBContextFull db = new DBContextFull();

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

        public async Task<ActionResult> Index(string searchTerm )
        {
            //DBContextFull db = new DBContextFull();

            List<EmployeesFull> employees;

            if (string.IsNullOrEmpty(searchTerm))
            {
                 employees = await db.EmployeesFulls.ToListAsync();             }
            else
            {
                employees = await db.EmployeesFulls.Where(x => x.FullName.StartsWith(searchTerm)).ToListAsync();
            }


            return View( );

        }
        [HttpPost]
        public ActionResult MyAction(string valueINeed)
        {
            DateTime startDate = DateTime.ParseExact(valueINeed, "yyyy-MM-dd ,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);
            //DBContextFull dbf = new DBContextFull();

            List<EmployeesFull> employees;

            if (startDate != null)
            {
                employees = db.EmployeesFulls.Where(l => l.JoiningDate <= startDate).ToList();
            }
            else
            {
                employees = db.EmployeesFulls.ToList();
            }
            return View(employees);
        }
        
        //RC review
        [HttpPost]
        public ActionResult MyAction(String StartDate, String EndDate)
        {
            DateTime startDate = DateTime.ParseExact(StartDate, "yyyy-MM-dd ,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);
            DateTime endtDate = DateTime.ParseExact(EndDate, "yyyy-MM-dd ,fff",
                                       System.Globalization.CultureInfo.InvariantCulture);
            //DBContextFull dbf = new DBContextFull();

            List<EmployeesFull> employees;

            if (startDate != null)
            {
                employees = db.EmployeesFulls.Where("JoiningDate >= startDate & JoiningDate <= endDate").ToList();
            }
            else
            {
                //empyty cost tam
                employees = db.EmployeesFulls.ToList();
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
            //DBContextFull dbf = new DBContextFull();

            List<EmployeesFull> employees;

            if (startDate != null)
            {
                employees = db.EmployeesFulls.Where(l => l.JoiningDate <= startDate).ToList();
            }
            else
            {
                employees = db.EmployeesFulls.ToList();
            }
            return View("View1", employees);

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
        
        public JsonResult FilterOutTableAfterDate(DateTime date)
        {
            //using (DBContextFull dbf = new DBContextFull())
            //{
                List<EmployeesFull> employees;

                if (date != null)
                {
                    employees = db.EmployeesFulls.Where(l => l.JoiningDate <= date).ToList();
                }
                else
                {
                    employees = db.EmployeesFulls.ToList();
                }

                return Json(employees, JsonRequestBehavior.AllowGet);
            //}
        }



    }
}
