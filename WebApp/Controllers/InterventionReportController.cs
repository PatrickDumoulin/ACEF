using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Logic.Interfaces;
using WebApp.ViewModels;
using System;
using System.Linq;
using CoreLib.Injection;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Superutilisateur, AdministrateurCA")]
    public class InterventionReportController : Controller
    {
        private readonly IInterventionBLL _interventionBLL;
        private readonly IMDBLL _mdBLL;
        private readonly IClientBLL _clientBLL;
        private readonly IEmployeeBLL _employeeBLL;

        // Injection des dépendances via le constructeur
        public InterventionReportController()
        {
            _interventionBLL = Injector.ImplementBll<IInterventionBLL>();
            _mdBLL = Injector.ImplementBll<IMDBLL>();
            _clientBLL = Injector.ImplementBll<IClientBLL>();
            _employeeBLL = Injector.ImplementBll<IEmployeeBLL>();
        }

        // Affichage initial de la page avec les filtres
        public IActionResult Index()
        {
            var model = new InterventionReportViewModel
            {
                StartDate = DateTime.Now.AddMonths(-1), // Mois dernier par défaut
                EndDate = DateTime.Now, // Aujourd'hui par défaut
                MinIncome = 10000, // Valeur par défaut pour revenu minimum
                MaxIncome = 60000, // Valeur par défaut pour revenu maximum
                AgeMin = 18, // Valeur par défaut pour l'âge minimum
                AgeMax = 75, // Valeur par défaut pour l'âge maximum
                IncomeInterval = 5000, // Intervalle pour le revenu
                AgeInterval = 10 // Intervalle pour l'âge
            };
            return View(model);
        }

        [HttpPost]
        public IActionResult GenerateReport(InterventionReportViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Récupérer la liste des interventions à partir de la réponse

                var interventions = _interventionBLL.GetInterventions().ElementList;

                // Appliquer les filtres sur les dates, revenus
                var filteredInterventions = interventions
                    .Where(i => i.DateIntervention >= model.StartDate && i.DateIntervention <= model.EndDate)
                    .Where(i => _interventionBLL.DecryptDebtAmount(i.DebtAmount) >= model.MinIncome && _interventionBLL.DecryptDebtAmount(i.DebtAmount) <= model.MaxIncome)
                    .ToList();

                // Mapper les résultats en InterventionViewModel avec filtre âge
                model.Interventions = filteredInterventions.Select(i => MapToViewModel(i)).Where(x => x.Age >= 18 && x.Age <= 75).ToList();

                return View("ReportResults", model);
            }

            return View("Index", model);
        }

        // Méthode pour calculer l'âge d'un client à partir de son ID
        private int GetClientAge(int clientId)
        {
            return _clientBLL.GetClientAge(clientId);
        }

        // Méthode privée pour mapper les objets InterventionBOL à la vue modèle
        private InterventionViewModel MapToViewModel(InterventionBOL bol)
        {
            return new InterventionViewModel
            {
                Id = bol.Id,
                DateIntervention = bol.DateIntervention,
                ClientName = bol.IdClient.HasValue ? _clientBLL.GetClientName(bol.IdClient.Value) : "Inconnu",
                EmployeeName = bol.IdEmployee.HasValue ? _employeeBLL.GetEmployeeName(bol.IdEmployee.Value) : "Inconnu",
                Income = bol.DebtAmount != null ? _interventionBLL.DecryptDebtAmount(bol.DebtAmount) : (decimal?)null,
                Age = bol.IdClient.HasValue ? GetClientAge(bol.IdClient.Value) : (int?)null
            };
        }



    }
}
