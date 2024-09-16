using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;  // Importation pour SelectList
using BusinessLayer.Logic.Interfaces;
using WebApp.ViewModels;
using System;
using System.Linq;
using CoreLib.Injection;

namespace WebApp.Controllers
{
    public class SeminarReportController : Controller
    {
        private readonly ISeminarBLL _seminarBLL;
        private readonly IMDBLL _mdBLL;
        private readonly IClientBLL _clientBLL;
        private readonly IEmployeeBLL _employeeBLL;

        // Injection des dépendances via le constructeur
        public SeminarReportController()
        {
            _seminarBLL = Injector.ImplementBll<ISeminarBLL>();
            _mdBLL = Injector.ImplementBll<IMDBLL>();
            _clientBLL = Injector.ImplementBll<IClientBLL>();
            _employeeBLL = Injector.ImplementBll<IEmployeeBLL>();
        }

        // Affichage initial de la page avec les filtres
        public IActionResult Index()
        {
            var model = new SeminarReportViewModel
            {
                StartDate = DateTime.Now.AddMonths(-1), // Mois dernier par défaut
                EndDate = DateTime.Now, // Aujourd'hui par défaut
            };

            // Charger les thèmes d'atelier pour le dropdown dans la vue
            var seminarThemes = _mdBLL.GetAllMdSeminarThemes().ElementList;  // Utilise ElementList ici
            ViewBag.SeminarThemes = new SelectList(seminarThemes, "Id", "Name");

            return View(model);
        }

        [HttpPost]
        [HttpPost]
        public IActionResult GenerateReport(SeminarReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Récupérer la liste des ateliers

                var seminars = _seminarBLL.GetAllSeminars().ElementList;

                // Préparer les données pour le rapport
                var seminarThemes = _mdBLL.GetAllMdSeminarThemes().ElementList;

                //liste a afficher
                var reportData = seminars
                    .Where(s => s.DateSeminar >= model.StartDate && s.DateSeminar <= model.EndDate)
                    .GroupBy(s => s.IdSeminarTheme)
                    .Select(group => new SeminarReportRow
                    {
                        Theme = seminarThemes.FirstOrDefault(t => t.Id == group.Key)?.Name ?? "Inconnu",
                        ParticipantCount = group.Sum(s => s.Participants.Count),
                        SeminarCount = group.Count()
                    })
                    .ToList();

                //somme des participants
                ViewBag.Participanttotal = seminars
                    .Where(s => s.DateSeminar >= model.StartDate && s.DateSeminar <= model.EndDate)
                    .SelectMany(s => s.Participants) // Combine toutes les listes de participants
                    .Count();

                //somme des ateliers
                ViewBag.Ateliertotal = seminars
                    .Where(s => s.DateSeminar >= model.StartDate && s.DateSeminar <= model.EndDate)
                    .Count();
                model.ReportRows = reportData;

                return View("ReportResults", model);
            }

            // Réafficher la vue en cas d'erreur
            ViewBag.SeminarThemes = new SelectList(_mdBLL.GetAllMdSeminarThemes().ElementList, "Id", "Name");
            return View("Index", model);
        }

    }
}
