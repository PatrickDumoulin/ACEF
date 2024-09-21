using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Logic.Interfaces;
using WebApp.ViewModels;
using WebApp.Core.Controllers;
using System;
using DataAccess.BOL.Seminar;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Models;
using CoreLib.Injection;

namespace WebApp.Controllers
{
    public class SeminarsController : AbstractBLLController<ISeminarBLL>
    {
        private readonly IClientBLL _clientBLL;
        private readonly IEmployeeBLL _employeeBLL;

        public SeminarsController() : base()
        {
            _clientBLL = Injector.ImplementBll<IClientBLL>();
            _employeeBLL = Injector.ImplementBll<IEmployeeBLL>();
        }

        public IActionResult Index(SeminarSearchViewModel searchModel, int? clientId = null)
        {
            var response = bll.GetAllSeminars();
            if (response.Succeeded)
            {
                var seminars = response.ElementList.AsQueryable();

                if (clientId.HasValue)
                {
                    ViewBag.ClientId = clientId;
                }

                // Filtrage par date
                if (!string.IsNullOrEmpty(searchModel.DateFilter))
                {
                    var currentDate = DateTime.Now;
                    switch (searchModel.DateFilter)
                    {
                        case "Semaine en cours":
                            // Commencer la semaine au lundi (par défaut, DayOfWeek commence au dimanche)
                            var startOfWeek = currentDate.AddDays(-(int)(currentDate.DayOfWeek == DayOfWeek.Sunday ? 6 : currentDate.DayOfWeek - DayOfWeek.Monday));
                            var endOfWeek = startOfWeek.AddDays(6); // Dimanche de la même semaine
                            seminars = seminars.Where(s => s.DateSeminar >= startOfWeek && s.DateSeminar <= endOfWeek);
                            break;

                        case "Mois en cours":
                            // Définir le début et la fin du mois
                            var startOfMonth = new DateTime(currentDate.Year, currentDate.Month, 1);
                            var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1); // Dernier jour du mois
                            seminars = seminars.Where(s => s.DateSeminar >= startOfMonth && s.DateSeminar <= endOfMonth);
                            break;

                        case "Intervalle":
                            if (searchModel.StartDate.HasValue && searchModel.EndDate.HasValue)
                            {
                                seminars = seminars.Where(s => s.DateSeminar >= searchModel.StartDate && s.DateSeminar <= searchModel.EndDate);
                            }
                            break;
                    }
                }

                // Filtrage par thème
                if (searchModel.IdSeminarTheme.HasValue)
                {
                    seminars = seminars.Where(s => s.IdSeminarTheme == searchModel.IdSeminarTheme);
                }

                // Filtrage par intervenant
                if (!string.IsNullOrEmpty(searchModel.IntervenantFilter))
                {
                    seminars = seminars.Where(s => s.Intervenants.Any(i => (i.FirstName + " " + i.LastName).Contains(searchModel.IntervenantFilter, StringComparison.OrdinalIgnoreCase)));
                }

                // Tri par date décroissante
                seminars = seminars.OrderByDescending(s => s.DateSeminar);

                // Récupération des thèmes pour la liste déroulante
                var themesResponse = bll.GetSeminarThemes();
                if (themesResponse.Succeeded)
                {
                    // Ajout à ViewBag pour l'utilisation dans la vue
                    ViewBag.SeminarThemes = themesResponse.ElementList.Select(t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.Name
                    }).ToList();

                    // Assignation au modèle de vue
                    searchModel.Themes = themesResponse.ElementList.Select(t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.Name
                    }).ToList();
                }

                // Pagination (simple exemple)
                int pageSize = 10; // nombre d'éléments par page
                int currentPage = searchModel.CurrentPage;
                int totalSeminars = seminars.Count();
                searchModel.TotalPages = (int)Math.Ceiling(totalSeminars / (double)pageSize);

                searchModel.Seminars = seminars
                    .Skip((currentPage - 1) * pageSize)
                    .Take(pageSize)
                    .ToList();

                return View(searchModel);
            }

            return NotFound();
        }



        public IActionResult Details(int id)
        {
            var response = bll.GetSeminarById(id);
            if (response.Succeeded && response.Element != null)
            {
                var seminar = response.Element;

                // Récupérer les thèmes pour ViewBag (comme dans la vue Index)
                var themesResponse = bll.GetSeminarThemes();
                if (themesResponse.Succeeded)
                {
                    ViewBag.SeminarThemes = themesResponse.ElementList.Select(t => new SelectListItem
                    {
                        Value = t.Id.ToString(),
                        Text = t.Name
                    }).ToList();
                }

                return View(seminar);
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            var currentUserName = User.Identity.Name;
            var currentEmployee = _employeeBLL.GetEmployeeByUsername(currentUserName);

            ViewBag.SeminarThemes = new SelectList(GetSeminarThemes(), "Id", "Name");
            ViewBag.Intervenants = new SelectList(GetIntervenants(), "Id", "FullName");
            ViewBag.Participants = new SelectList(GetParticipants(), "Id", "FullName");

            var model = new SeminarViewModel
            {
                SelectedParticipants = new List<int>(),
                SelectedIntervenants = new List<int>()
            };

            if (currentEmployee.Succeeded && currentEmployee.Element != null)
            {
                model.SelectedIntervenants.Add(currentEmployee.Element.Id);
            }

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SeminarViewModel model)
        {

            // Vérifier si au moins un participant est sélectionné
            if (model.SelectedParticipants == null || !model.SelectedParticipants.Any())
            {
                ModelState.AddModelError("SelectedParticipants", "Veuillez sélectionner au moins un participant.");
            }

            // Vérifier si au moins un intervenant est sélectionné
            if (model.SelectedIntervenants == null || !model.SelectedIntervenants.Any())
            {
                ModelState.AddModelError("SelectedIntervenants", "Veuillez sélectionner au moins un intervenant.");
            }

            // Si le modèle est valide, procéder à la création du séminaire
            if (ModelState.IsValid)
            {
                var seminar = new SeminarBOL
                (
                    model.DateSeminar,
                    model.IdSeminarTheme,
                    model.Notes,
                    DateTime.Now
                );

                // Ajouter les participants et les intervenants
                seminar.Participants = model.SelectedParticipants.Select(id => new Clients { Id = id }).ToList();
                seminar.Intervenants = model.SelectedIntervenants.Select(id => new Employees { Id = id }).ToList();

                TempData["success"] = "Atelier créé avec succès";
                // Appel au BLL pour créer le séminaire
                bll.CreateSeminar(seminar, seminar.Intervenants);

                // Rediriger vers la liste des séminaires
                return RedirectToAction(nameof(Index));
            }

            // S'il y a des erreurs, recharger les données de la vue
            ViewBag.SeminarThemes = new SelectList(GetSeminarThemes(), "Id", "Name");

            // Pré-sélectionner l'intervenant connecté
            var currentUserName = User.Identity.Name;
            var currentEmployee = _employeeBLL.GetEmployeeByUsername(currentUserName);

            // Accéder à l'élément dans GetItemResponse<Employees> avant de récupérer l'Id
            if (currentEmployee.Succeeded && currentEmployee.Element != null)
            {
                model.SelectedIntervenants = new List<int> { currentEmployee.Element.Id };
            }

            ViewBag.Intervenants = new SelectList(GetIntervenants(), "Id", "FullName", currentEmployee?.Element?.Id);
            ViewBag.Participants = new SelectList(GetParticipants(), "Id", "FullName");



            return View(model);
        }


        public IActionResult Edit(int id)
        {
            var response = bll.GetSeminarById(id);
            if (response.Succeeded && response.Element != null)
            {
                var seminar = response.Element;

                var model = new SeminarViewModel
                {
                    Id = seminar.Id,
                    DateSeminar = seminar.DateSeminar ?? DateTime.Now,  // Utilisation de la date si elle est définie
                    IdSeminarTheme = seminar.IdSeminarTheme ?? 0,
                    Notes = seminar.Notes,
                    SelectedParticipants = seminar.Participants.Select(p => p.Id).ToList(),
                    SelectedIntervenants = seminar.Intervenants.Select(i => i.Id).ToList(),
                };

                // Remplissage des ViewBag pour les listes déroulantes
                ViewBag.SeminarThemes = new SelectList(GetSeminarThemes(), "Id", "Name");
                ViewBag.Intervenants = new SelectList(GetIntervenants(), "Id", "FullName");
                ViewBag.Participants = new SelectList(GetParticipants(), "Id", "FullName");

                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(SeminarViewModel model)
        {
            // Vérifier si au moins un participant est sélectionné
            if (model.SelectedParticipants == null || !model.SelectedParticipants.Any())
            {
                ModelState.AddModelError("SelectedParticipants", "Veuillez sélectionner au moins un participant.");
            }

            // Vérifier si au moins un intervenant est sélectionné
            if (model.SelectedIntervenants == null || !model.SelectedIntervenants.Any())
            {
                ModelState.AddModelError("SelectedIntervenants", "Veuillez sélectionner au moins un intervenant.");
            }

            if (ModelState.IsValid)
            {
                var seminar = new SeminarBOL
                {
                    Id = model.Id,
                    DateSeminar = model.DateSeminar,
                    IdSeminarTheme = model.IdSeminarTheme,
                    Notes = model.Notes,
                    Participants = model.SelectedParticipants.Select(id => new Clients { Id = id }).ToList(),
                    Intervenants = model.SelectedIntervenants.Select(id => new Employees { Id = id }).ToList()
                };

                // Appel au BLL pour mettre à jour le séminaire
                TempData["success"] = "Atelier modifié avec succès";
                bll.UpdateSeminar(seminar);

                return RedirectToAction(nameof(Index));
            }

            // Si la validation échoue, on recharge les listes déroulantes
            ViewBag.SeminarThemes = new SelectList(GetSeminarThemes(), "Id", "Name");
            ViewBag.Intervenants = new SelectList(GetIntervenants(), "Id", "FullName");
            ViewBag.Participants = new SelectList(GetParticipants(), "Id", "FullName");

            return View(model);
        }


        public IActionResult Delete(int id)
        {
            var response = bll.GetSeminarById(id);
            if (response.Succeeded && response.Element != null)
            {
                return View(response.Element);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("Seminars/DeleteConfirmed/{id}")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                bll.DeleteSeminar(id);
                TempData["SuccessMessage"] = "Le séminaire a été supprimé avec succès.";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Une erreur est survenue lors de la suppression : {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }


        private IEnumerable<MdSeminarThemes> GetSeminarThemes()
        {
            var response = bll.GetSeminarThemes();
            if (response.Succeeded)
            {
                return response.ElementList;
            }
            return new List<MdSeminarThemes>();
        }

        [HttpGet]
        public JsonResult SearchParticipants(string searchTerm)
        {
            var participants = GetParticipants().Where(p =>
                p.FullName.ToLower().Contains(searchTerm.ToLower())).ToList();
            return Json(participants);
        }

        private IEnumerable<ClientViewModel> GetParticipants()
        {
            var response = _clientBLL.GetClients();
            if (response.Succeeded)
            {
                return response.ElementList.Select(c => new ClientViewModel
                {
                    Id = c.Id,
                    FullName = $"{c.FirstName} {c.LastName}"
                });
            }
            return new List<ClientViewModel>();
        }

        [HttpGet]
        public JsonResult SearchIntervenants(string searchTerm)
        {
            var intervenants = GetIntervenants().Where(p =>
                p.FullName.ToLower().Contains(searchTerm.ToLower())).ToList();
            return Json(intervenants);
        }

        private IEnumerable<ClientViewModel> GetIntervenants()
        {
            var response = _employeeBLL.GetAllEmployees();
            if (response.Succeeded)
            {
                return response.ElementList.Select(c => new ClientViewModel
                {
                    Id = c.Id,
                    FullName = $"{c.FirstName} {c.LastName}"
                });
            }
            return new List<ClientViewModel>();
        }
    }
}
