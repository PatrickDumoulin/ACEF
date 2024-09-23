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

                foreach (var employee in employeeList)
                {
                    var user = _userManager.FindByNameAsync(employee.UserName).Result;
                    var roles = _userManager.GetRolesAsync(user).Result;
                    employee.Role = string.Join(", ", roles); // Combiner les rôles en une seule chaîne
                }

                // Options de tri
                ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
                ViewBag.FirstNameSortParm = sortOrder == "FirstName" ? "firstName_desc" : "FirstName";
                ViewBag.LastNameSortParm = sortOrder == "LastName" ? "lastName_desc" : "LastName";
                ViewBag.LastLoginDateSortParm = sortOrder == "LastLoginDate" ? "lastLoginDate_desc" : "LastLoginDate";

                // Appliquer le tri selon l'option choisie
                switch (sortOrder)
                {
                    case "id_desc":
                        employeeList = employeeList.OrderByDescending(e => e.Id).ToList(); // Trier par ID décroissant
                        break;
                    case "FirstName":
                        employeeList = employeeList.OrderBy(e => e.FirstName).ToList();
                        break;
                    case "firstName_desc":
                        employeeList = employeeList.OrderByDescending(e => e.FirstName).ToList();
                        break;
                    case "LastName":
                        employeeList = employeeList.OrderBy(e => e.LastName).ToList();
                        break;
                    case "lastName_desc":
                        employeeList = employeeList.OrderByDescending(e => e.LastName).ToList();
                        break;
                    case "LastLoginDate":
                        employeeList = employeeList.OrderBy(e => e.LastLoginDate).ToList();
                        break;
                    case "lastLoginDate_desc":
                        employeeList = employeeList.OrderByDescending(e => e.LastLoginDate).ToList();
                        break;
                    default:
                        employeeList = employeeList.OrderByDescending(e => e.Id).ToList(); // Trier par ID décroissant par défaut (les plus récents)
                        break;
                }

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
            if (!model.HasRoleSelected)
            {
                ModelState.AddModelError(string.Empty, "Veuillez sélectionner au moins un rôle.");
            }

            ModelState.Remove("PasswordHash");
            ModelState.Remove("Role");
            if (ModelState.IsValid)
            {
                var tempPassword = model.NewPassword ?? GenerateSecurePassword();

                var user = new ApplicationUser
                {
                    UserName = model.UserName, // Utilise l'adresse e-mail pour remplir le champ UserName
                    Email = model.UserName,    // Stocke également l'e-mail dans le champ Email
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
                    

                    // Vérifie si l'erreur concerne le nom d'utilisateur déjà pris
                    if (error.Code == "DuplicateUserName")
                    {
                        ModelState.AddModelError("", $"Le nom d'utilisateur '{model.UserName}' est déjà utilisé.");
                    }
                    else
                    {
                        // Ajoute l'erreur originale (ou tu peux choisir de traduire d'autres erreurs)
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }



        public async Task<IActionResult> Edit(int id)
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
                    NewPassword = "",
                    //PasswordHash = response.Element.PasswordHash,
                    LastLoginDate = response.Element.LastLoginDate,
                    Active = response.Element.Active,
                    //Email = user.Email,
                    IsSuperUser = await _userManager.IsInRoleAsync(user, "Superutilisateur"),
                    IsIntervenant = await _userManager.IsInRoleAsync(user, "Intervenant"),
                    IsAdministrateurCA = await _userManager.IsInRoleAsync(user, "AdministrateurCA")
                };

                
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EmployeeViewModel model)
        {
            if (!model.HasRoleSelected)
            {
                ModelState.AddModelError(string.Empty, "Veuillez sélectionner au moins un rôle.");
            }
            // Retirer l'erreur de validation pour NewPassword
            ModelState.Remove("PasswordHash");
            ModelState.Remove("NewPassword");
            ModelState.Remove("ConfirmPassword");
            ModelState.Remove("Role");
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
                            //user.IsFirstLogin = true;
                            await _userManager.UpdateAsync(user);
                        }

                        // Mise à jour des rôles
                        await UpdateRoles(user, model);

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
                        

                        // Vérifie si l'erreur concerne le nom d'utilisateur déjà pris
                        if (error.Code == "DuplicateUserName")
                        {
                            ModelState.AddModelError("", $"Le nom d'utilisateur '{model.UserName}' est déjà utilisé.");
                        }
                        else
                        {
                            // Ajoute l'erreur originale (ou tu peux choisir de traduire d'autres erreurs)
                            ModelState.AddModelError("", error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Employé introuvable.");
                }
            }
            return View(model);
        }


        // GET: ChangePassword
        public async Task<IActionResult> ChangePassword(int id)
        {
            ViewData["IdEmployeeRetour"] = id;
            var response = bll.GetEmployeeById(id);
            if (response.Succeeded && response.Element != null)
            {
                var model = new ChangePasswordEmployeeViewModel
                {
                    // Aucune valeur à pré-remplir, car nous ne demandons que le nouveau mot de passe
                };
                return View(model);
            }
            return NotFound();
        }

        // POST: ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(int id, ChangePasswordEmployeeViewModel model)
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

                    // Supprimer l'ancien mot de passe et définir le nouveau mot de passe
                    var removePasswordResult = await _userManager.RemovePasswordAsync(user);
                    if (!removePasswordResult.Succeeded)
                    {
                        foreach (var error in removePasswordResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(model);
                    }

                    // Ajouter le nouveau mot de passe
                    var addPasswordResult = await _userManager.AddPasswordAsync(user, model.NewPassword);
                    // Marquer l'utilisateur comme nayant PAS besoin de changer le mot de passe à la prochaine connexion
                    
                    if (addPasswordResult.Succeeded)
                    {
                        user.IsFirstLogin = false;
                        TempData["success"] = "Mot de passe modifié avec succès.";
                        return RedirectToAction(nameof(Index));
                    }

                    foreach (var error in addPasswordResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Utilisateur introuvable.");
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
