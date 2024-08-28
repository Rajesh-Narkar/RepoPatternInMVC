using Microsoft.AspNetCore.Mvc;
using RepositoryPatternMVC.Models;
using RepositoryPatternMVC.Repo;
namespace RepositoryPatternMVC.Controllers
{
    public class EmpController : Controller
    {
        private readonly EmpRepo repo;
        public EmpController(EmpRepo repo)
        {
            this.repo = repo;
        }
        public IActionResult Index()
        {
            var dt=repo.GetAllEmps();
            return View(dt);
        }
      
        public IActionResult AddEmp()
        {          
            return View();
       
        }
        [HttpPost]
        public IActionResult AddEmp(Emp e)
        {
            if (ModelState.IsValid)
            {
                repo.NewEmp(e);
                TempData["msg"] = "Emp Added Successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View();
            }           
        }

        public IActionResult EditEmp(int id)
        {
            var employee = repo.GetEmpById(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        [HttpPost]
        public IActionResult EditEmp(Emp e)
        {
            if (ModelState.IsValid)
            {
                repo.ModifyEmp(e);
                TempData["msg"] = "Employee updated successfully.";
                return RedirectToAction("Index");
            }
            return View(e);
        }
        public IActionResult DeleteEmp(int id)
        {
            repo.RemoveEmp(id);
            return RedirectToAction("Index");
        }

        public IActionResult SearchEmp(string searchTerm, bool backToList = false)
        {
            if (backToList)
            {
                return RedirectToAction("Index");
            }

            var employees = repo.SearchEmp(searchTerm);
            return View("Index", employees);
        }
    }
}
