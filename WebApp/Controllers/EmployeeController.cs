using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Logic.Interfaces;
using WebApp.ViewModels;
using WebApp.Core.Controllers;
using System.Linq;
using DataAccess.Models;

namespace WebApp.Controllers
{
    public class EmployeeController : AbstractBLLController<IEmployeeBLL>
    {
        public EmployeeController() : base() { }

        public IActionResult Index()
        {
            var response = bll.GetAllEmployees();
            if (response.Succeeded)
            {
                var viewModelList = response.ElementList.Select(e => new EmployeeViewModel
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    UserName = e.UserName,
                    PasswordHash = e.PasswordHash,
                    LastLoginDate = e.LastLoginDate,
                    Active = e.Active
                }).ToList();

                return View(viewModelList);
            }
            return NotFound();
        }

        public IActionResult Details(int id)
        {
            var response = bll.GetEmployeeById(id);
            if (response.Succeeded && response.Element != null)
            {
                var model = new EmployeeViewModel
                {
                    Id = response.Element.Id,
                    FirstName = response.Element.FirstName,
                    LastName = response.Element.LastName,
                    UserName = response.Element.UserName,
                    PasswordHash = response.Element.PasswordHash,
                    LastLoginDate = response.Element.LastLoginDate,
                    Active = response.Element.Active
                };
                return View(model);
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employees
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    PasswordHash = model.PasswordHash,
                    LastLoginDate = DateTime.Now,
                    Active = model.Active
                };

                bll.CreateEmployee(employee); // Pass the Employees object directly
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var response = bll.GetEmployeeById(id);
            if (response.Succeeded && response.Element != null)
            {
                var model = new EmployeeViewModel
                {
                    Id = response.Element.Id,
                    FirstName = response.Element.FirstName,
                    LastName = response.Element.LastName,
                    UserName = response.Element.UserName,
                    PasswordHash = response.Element.PasswordHash,
                    LastLoginDate = response.Element.LastLoginDate,
                    Active = response.Element.Active
                };
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var employee = new Employees
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.UserName,
                    PasswordHash = model.PasswordHash,
                    LastLoginDate = model.LastLoginDate,
                    Active = model.Active
                };

                bll.UpdateEmployee(employee); // Pass the Employees object directly
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var response = bll.GetEmployeeById(id);
            if (response.Succeeded && response.Element != null)
            {
                var model = new EmployeeViewModel
                {
                    Id = response.Element.Id,
                    FirstName = response.Element.FirstName,
                    LastName = response.Element.LastName,
                    UserName = response.Element.UserName,
                    PasswordHash = response.Element.PasswordHash,
                    LastLoginDate = response.Element.LastLoginDate,
                    Active = response.Element.Active
                };
                return View(model);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            bll.DeleteEmployee(id);
            return RedirectToAction(nameof(Index));
        }

    }
}
