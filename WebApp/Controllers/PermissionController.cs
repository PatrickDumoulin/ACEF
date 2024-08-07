using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Logic.Interfaces;
using WebApp.ViewModels;
using WebApp.Core.Controllers;
using System.Linq;
using DataAccess.BOL.EmployeePermission;
using Microsoft.AspNetCore.Mvc.Rendering;
using CoreLib.Injection;
using System;

namespace WebApp.Controllers
{
    public class PermissionController : AbstractBLLController<IEmployeePermissionsBLL>
    {
        private readonly IEmployeeBLL _employeeBLL;

        public PermissionController() : base()
        {
            _employeeBLL = Injector.ImplementBll<IEmployeeBLL>();
        }

        public IActionResult Index()
        {
            var response = bll.GetAllPermissions();
            if (response.Succeeded)
            {
                var viewModelList = response.ElementList.Select(p => new EmployeePermissionsViewModel
                {
                    Id = p.Id,
                    EmployeeId = p.EmployeeId ?? 0,
                    AllowInterventions = p.AllowInterventions ?? false,
                    AllowReports = p.AllowReports ?? false,
                    AllowSu = p.AllowSu ?? false
                }).ToList();

                return View(viewModelList);
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            var model = new EmployeePermissionsViewModel
            {
                Employees = GetEmployeesSelectList()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(EmployeePermissionsViewModel model)
        {
            Console.WriteLine("Form submitted"); // Log simple pour vérifier si la méthode est atteinte
            if (ModelState.IsValid)
            {
                Console.WriteLine("Model is valid"); // Log pour vérifier la validité du modèle
                var permissionBOL = new EmployeePermissionsBOL(new DataAccess.Models.EmployeePermissions
                {
                    EmployeeId = model.EmployeeId,
                    AllowInterventions = model.AllowInterventions,
                    AllowReports = model.AllowReports,
                    AllowSu = model.AllowSu
                });

                bll.CreatePermission(permissionBOL.Record);
                return RedirectToAction(nameof(Index));
            }
            Console.WriteLine("Model is invalid"); // Log pour vérifier si le modèle est invalide
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine("Validation error: " + error.ErrorMessage); // Log des erreurs de validation
            }
            model.Employees = GetEmployeesSelectList(); // Recharger la liste des employés en cas d'erreur de validation
            return View(model);
        }

        private SelectList GetEmployeesSelectList()
        {
            var response = _employeeBLL.GetAllEmployees();
            if (response.Succeeded)
            {
                var employees = response.ElementList.Select(e => new
                {
                    Id = e.Id,
                    FullName = $"{e.FirstName} {e.LastName}"
                }).ToList();

                return new SelectList(employees, "Id", "FullName");
            }
            return new SelectList(Enumerable.Empty<SelectListItem>());
        }

        public IActionResult Edit(int id)
        {
            var response = bll.GetPermissionById(id);
            if (response.Succeeded && response.Element != null)
            {
                var model = new EmployeePermissionsViewModel
                {
                    Id = response.Element.Id,
                    EmployeeId = response.Element.EmployeeId ?? 0,
                    AllowInterventions = response.Element.AllowInterventions ?? false,
                    AllowReports = response.Element.AllowReports ?? false,
                    AllowSu = response.Element.AllowSu ?? false,
                    Employees = GetEmployeesSelectList()
                };
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, EmployeePermissionsViewModel model)
        {
            if (ModelState.IsValid)
            {
                var permissionBOL = new EmployeePermissionsBOL(new DataAccess.Models.EmployeePermissions
                {
                    Id = model.Id,
                    EmployeeId = model.EmployeeId,
                    AllowInterventions = model.AllowInterventions,
                    AllowReports = model.AllowReports,
                    AllowSu = model.AllowSu
                });

                bll.UpdatePermission(permissionBOL.Record);
                return RedirectToAction(nameof(Index));
            }
            model.Employees = GetEmployeesSelectList(); // Recharger la liste des employés en cas d'erreur de validation
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var response = bll.GetPermissionById(id);
            if (response.Succeeded && response.Element != null)
            {
                return View(response.Element);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            bll.DeletePermission(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
