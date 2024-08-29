using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Logic.Interfaces;
using WebApp.ViewModels;
using WebApp.Core.Controllers;
using System.Linq;
using DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using WebApp.Models;
using System.Text;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
   [Authorize(Roles = "Superutilisateur")]
    public class EmployeeController : AbstractBLLController<IEmployeeBLL>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public EmployeeController(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager) : base()
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _roleManager = roleManager ?? throw new ArgumentNullException(nameof(roleManager));
        }

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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Utiliser le mot de passe fourni ou générer un mot de passe temporaire sécurisé
                var tempPassword = model.PasswordHash ?? GenerateSecurePassword();

                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    LastLoginDate = DateTime.Now,
                    Active = model.Active ?? false,
                    IsFirstLogin = true // Forcer le changement de mot de passe à la première connexion
                };

                var result = await _userManager.CreateAsync(user, tempPassword);
                if (result.Succeeded)
                {
                    // Synchroniser avec la table Employees
                    var employee = new Employees
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.UserName,
                        PasswordHash = user.PasswordHash,
                        LastLoginDate = user.LastLoginDate.Value,
                        Active = user.Active
                    };

                    bll.CreateEmployee(employee);

                    // Ajouter l'utilisateur aux rôles (par exemple, Superutilisateur)
                    if (model.IsSuperUser)
                    {
                        await _userManager.AddToRoleAsync(user, "Superutilisateur");
                    }
                    if (model.IsIntervenant)
                    {
                        await _userManager.AddToRoleAsync(user, "Intervenant");
                    }
                    if (model.IsAdministrateurCA)
                    {
                        await _userManager.AddToRoleAsync(user, "AdministrateurCA");
                    }

                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);
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
        public async Task<IActionResult> Edit(int id, EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                var response = bll.GetEmployeeById(id);
                if (response.Succeeded && response.Element != null)
                {
                    var user = await _userManager.FindByNameAsync(response.Element.UserName);
                    if (user == null)
                    {
                        return NotFound();
                    }

                    user.FirstName = model.FirstName;
                    user.LastName = model.LastName;
                    user.UserName = model.UserName;
                    user.Email = model.UserName;
                    user.Active = model.Active ?? false;
                    user.LastLoginDate = DateTime.Now; //A CORRIGER PLUS TARD 

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        var employee = new Employees
                        {
                            Id = id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            UserName = user.UserName,
                            PasswordHash = user.PasswordHash,
                            LastLoginDate = user.LastLoginDate.Value,
                            Active = user.Active
                        };

                        bll.UpdateEmployee(employee);

                        // Mettre à jour les rôles
                        await UpdateRoles(user, model);

                        return RedirectToAction(nameof(Index));
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                    
            }
            return View(model);
        }

        private async Task UpdateRoles(ApplicationUser user, EmployeeViewModel model)
        {
            // Mise à jour des rôles pour l'utilisateur
            await UpdateUserRoleAsync(user, "Superutilisateur", model.IsSuperUser);
            await UpdateUserRoleAsync(user, "Intervenant", model.IsIntervenant);
            await UpdateUserRoleAsync(user, "AdministrateurCA", model.IsAdministrateurCA);
        }

        private async Task UpdateUserRoleAsync(ApplicationUser user, string roleName, bool assignRole)
        {
            if (assignRole)
            {
                if (!await _userManager.IsInRoleAsync(user, roleName))
                {
                    await _userManager.AddToRoleAsync(user, roleName);
                }
            }
            else
            {
                if (await _userManager.IsInRoleAsync(user, roleName))
                {
                    await _userManager.RemoveFromRoleAsync(user, roleName);
                }
            }
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
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            // Récupérer l'employé dans la table Employees
            var response = bll.GetEmployeeById(id);
            if (response.Succeeded && response.Element != null)
            {
                var employee = response.Element;

                // Récupérer l'utilisateur dans AspNetUsers en utilisant le UserName
                var user = await _userManager.FindByNameAsync(employee.UserName);
                if (user != null)
                {
                    // Supprimer l'utilisateur de la table AspNetUsers
                    var result = await _userManager.DeleteAsync(user);
                    if (result.Succeeded)
                    {
                        // Supprimer l'employé de la table Employees
                        bll.DeleteEmployee(id);
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Utilisateur introuvable dans la table AspNetUsers.");
                }
            }
            else
            {
                ModelState.AddModelError("", "Employé introuvable dans la table Employees.");
            }

            return RedirectToAction(nameof(Index));
        }

        private string GenerateSecurePassword(int length = 12)
        {
            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890!@#$%^&*()_-+=<>?";
            StringBuilder res = new StringBuilder();
            Random rnd = new Random();
            while (0 < length--)
            {
                res.Append(valid[rnd.Next(valid.Length)]);
            }
            return res.ToString();
        }
       
    }
}
