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
using BusinessLayer.Communication.Responses.Interfaces;

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

        public async Task<IActionResult> Details(int id)
        {
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
            var model = new EmployeeViewModel
            {
                PasswordHash = GenerateSecurePassword() // Génération du mot de passe aléatoire
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(EmployeeViewModel model)
        {
            ModelState.Remove("NewPassword");
            if (ModelState.IsValid)
            {
                var tempPassword = model.PasswordHash ?? GenerateSecurePassword();

                var user = new ApplicationUser
                {
                    UserName = model.Email, // Utilise l'adresse e-mail pour remplir le champ UserName
                    Email = model.Email,    // Stocke également l'e-mail dans le champ Email
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    LastLoginDate = DateTime.Now,
                    Active = model.Active ?? false,
                    IsFirstLogin = true // L'utilisateur devra changer son mot de passe à la première connexion
                };

                var result = await _userManager.CreateAsync(user, tempPassword);
                if (result.Succeeded)
                {
                    // Synchronisation avec la table Employees
                    var employee = new Employees
                    {
                        FirstName = user.FirstName,
                        LastName = user.LastName,
                        UserName = user.UserName, // Le UserName contient maintenant l'adresse e-mail
                        PasswordHash = user.PasswordHash,
                        LastLoginDate = user.LastLoginDate.Value,
                        Active = user.Active
                    };

                    bll.CreateEmployee(employee);

                    // Ajouter l'utilisateur aux rôles
                    if (model.IsSuperUser)
                        await _userManager.AddToRoleAsync(user, "Superutilisateur");
                    if (model.IsIntervenant)
                        await _userManager.AddToRoleAsync(user, "Intervenant");
                    if (model.IsAdministrateurCA)
                        await _userManager.AddToRoleAsync(user, "AdministrateurCA");

                    TempData["success"] = "Employé créé avec succès";
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
            // Retirer l'erreur de validation pour NewPassword
            ModelState.Remove("PasswordHash");

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
                    user.Email = model.Email;
                    user.Active = model.Active ?? true;


                    var result = await _userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        // Si un nouveau mot de passe est spécifié, le mettre à jour
                        if (!string.IsNullOrEmpty(model.NewPassword))
                        {
                            var removePasswordResult = await _userManager.RemovePasswordAsync(user);
                            if (!removePasswordResult.Succeeded)
                            {
                                foreach (var error in removePasswordResult.Errors)
                                {
                                    ModelState.AddModelError("", error.Description);
                                }
                                return View(model);
                            }

                            var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
                            if (!addPasswordResult.Succeeded)
                            {
                                foreach (var error in addPasswordResult.Errors)
                                {
                                    ModelState.AddModelError("", error.Description);
                                }
                                return View(model);
                            }

                            // Marquer l'utilisateur comme ayant besoin de changer le mot de passe à la prochaine connexion
                            user.IsFirstLogin = true;
                            await _userManager.UpdateAsync(user);
                        }

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

                        await UpdateRoles(user, model);

                        TempData["success"] = "Employé modifié avec succès";
                        return RedirectToAction(nameof(Index));
                    }

                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Employé introuvable.");
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

        public async Task<IActionResult> ToggleActive (int employeeId)
        {
            var employee = bll.GetEmployeeById(employeeId);
            var user = await _userManager.FindByNameAsync(employee.Element.UserName);

            var userRoles = await _userManager.GetRolesAsync(user);

            // Vérifier si l'utilisateur est Superutilisateur
            bool isSuperUser = userRoles.Contains("Superutilisateur");

            // Passer l'information au ViewData
            ViewData["IsSuperUser"] = isSuperUser;

            if (employee.Succeeded && employee.Element != null)
            {
                employee.Element.Active = !employee.Element.Active;
                user.Active = !user.Active;

                base.bll.UpdateEmployee(employee.Element);
                await _userManager.UpdateAsync(user);
                TempData["success"] = "Statut changé avec succès";

                return RedirectToAction("Index");
            }


            return NotFound();
        }

    }
}
