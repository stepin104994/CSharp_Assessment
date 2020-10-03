using SampleWebApp.Models;
using System.Linq;
using System.Web.Mvc;

namespace SampleMvcApp.Controllers
{
    public class EmpDataController : Controller
    {
        public ViewResult AllEmployees()
        {
            var context = new MyDatabaseEntities();
            var model = context.EmpTables.ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult NewEmployee(EmpTable emp)
        {
            var context = new MyDatabaseEntities();
            context.EmpTables.Add(emp);
            context.SaveChanges();
            return RedirectToAction("AllEmployees");
        }

        public ActionResult Delete(string id)
        {
            int empId = int.Parse(id);
            var context = new MyDatabaseEntities();
            var model = context.EmpTables.FirstOrDefault((e) => e.EmpId == empId);
            context.EmpTables.Remove(model);
            context.SaveChanges();
            return RedirectToAction("AllEmployees");
        }

        public ViewResult Find(string id)
        {
            int empId = int.Parse(id);
            var context = new MyDatabaseEntities();
            var model = context.EmpTables.FirstOrDefault((e) => e.EmpId == empId);
            return View(model);

        }
        [HttpPost]
        public ActionResult Find(EmpTable emp)
        {
            var context = new MyDatabaseEntities();
            var model = context.EmpTables.FirstOrDefault((e) => e.EmpId == emp.EmpId);
            model.EmpName = emp.EmpName;
            model.EmpAddress = emp.EmpAddress;
            model.EmpSalary = emp.EmpSalary;  
            context.SaveChanges();
            return RedirectToAction("AllEmployees");
        }

        public ViewResult NewEmployee()
        {
            var model = new EmpTable();
            return View(model);
        }

        
    }
}