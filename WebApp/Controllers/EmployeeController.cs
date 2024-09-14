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

        public IActionResult Index(string sortOrder, int page = 1, int pageSize = 6)
        {
            var response = bll.GetAllEmployees();
            if (response.Succeeded)
            {
                var employeeList = response.ElementList.Select(e => new EmployeeViewModel
                {
                    Id = e.Id,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    UserName = e.UserName,
                    PasswordHash = e.PasswordHash,
                    LastLoginDate = e.LastLoginDate,
                    Active = e.Active,

                }).ToList();

                // Calculer le nombre total d'employés
                var totalCount = employeeList.Count;
                var totalPages = (int)Math.Ceiling((decimal)totalCount / pageSize);

                var pagedEmployees = employeeList.Skip((page - 1) * pageSize).Take(pageSize).ToList();

                var viewModel = new EmployeeIndexViewModel
                {
                    Employees = pagedEmployees,  // Liste paginée
                    CurrentPage = page,
                    TotalPages = totalPages,
                };

                return View(viewModel);
            }
            return NotFound();
        }

        

        

        public async Task<IActionResult> Details(int id, int? employeeId = null)
        {
            if (employeeId.HasValue)
            {
                // Indique que l'utilisateur vient de la page de création du séminaire
                ViewBag.ReturnToSeminarCreation = true;
            }

            var response = bll.GetEmployeeById(id);
            if (response.Succeeded && response.Element != null)
            {
                var user = await _userManager.FindByNameAsync(response.Element.UserName);

                var model = new EmployeeViewModel
                {
                    Id = response.Element.Id,
                    FirstName = response.Element.FirstName,
                    LastName = response.Element.LastName,
                    UserName = response.Element.UserName,
                    Active = response.Element.Active,
                    IsSuperUser = await _userManager.IsInRoleAsync(user, "Superutilisateur"),
                    IsIntervenant = await _userManager.IsInRoleAsync(user, "Intervenant"),
                    IsAdministrateurCA = await _userManager.IsInRoleAsync(user, "AdministrateurCA")
                };

                return View(model);
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            var model = new EmployeeViewModel();
            return View(model);
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
                    Email = model.UserName,
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

                    // Ajouter l'utilisateur aux rôles 
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

                    TempData["success"] = "Employé crée avec succès";
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
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

                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        // Si un nouveau mot de passe est spécifié dans le modèle, nous devons le mettre à jour
                        if (!string.IsNullOrEmpty(model.PasswordHash))
                        {
                            // Supprimez l'ancien mot de passe et ajoutez le nouveau
                            var removePasswordResult = await _userManager.RemovePasswordAsync(user);
                            if (!removePasswordResult.Succeeded)
                            {
                                foreach (var error in removePasswordResult.Errors)
                                {
                                    ModelState.AddModelError("", error.Description);
                                }
                                return View(model);
                            }

                            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.PasswordHash);
                            if (!addPasswordResult.Succeeded)
                            {
                                foreach (var error in addPasswordResult.Errors)
                                {
                                    ModelState.AddModelError("", error.Description);
                                }
                                return View(model);
                            }
                        }

                        // Mettre à jour l'employé dans la table Employees
                        var employee = new Employees
                        {
                            Id = id,
                            FirstName = user.FirstName,
                            LastName = user.LastName,
                            UserName = user.UserName,
                            PasswordHash = user.PasswordHash,
                            LastLoginDate = user.LastLoginDate ?? DateTime.Now,
                            Active = user.Active
                        };

                        bll.UpdateEmployee(employee);

                        // Mettre à jour les rôles
                        await UpdateRoles(user, model);

                        TempData["success"] = "Employé modifié avec succès";
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

        [HttpDelete]
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
