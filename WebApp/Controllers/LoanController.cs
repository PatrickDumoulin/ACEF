using BusinessLayer.Logic;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Injection;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Superutilisateur, AdministrateurCA")]
    public class LoanController : Controller
    {
        private readonly IInterventionBLL _interventionBLL;
        private readonly IMDBLL _mdBLL;
        private readonly IClientBLL _clientBLL;

        public LoanController()
        {
            _interventionBLL = Injector.ImplementBll<IInterventionBLL>();
            _mdBLL = Injector.ImplementBll<IMDBLL>();
            _clientBLL = Injector.ImplementBll<IClientBLL>();
        }

        public ActionResult LoanStatistics(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                // Si les dates ne sont pas fournies, définissez une plage de dates par défaut
                if (!startDate.HasValue || !endDate.HasValue)
                {
                    startDate = DateTime.MinValue;
                    endDate = DateTime.MaxValue;
                }

                if (startDate > endDate)
                {
                    ModelState.AddModelError("", "La date de début ne peut pas être postérieure à la date de fin.");
                    return View();
                }

                var allInterventionSolutions = _mdBLL.GetAllMdInterventionSolutions().ElementList
                    .Where(s => s.Name == "Petit Prêt")
                    .Select(s => s.Id)
                    .ToHashSet();

                var response = _interventionBLL.GetInterventions();
                var loanInterventions = response.ElementList
                    .Where(i => i.DateIntervention >= startDate && i.DateIntervention <= endDate
                                && i.InterventionsInterventionSolutions.Any(s => allInterventionSolutions.Contains(s.IdInterventionSolution)))
                    .ToList();

                // Debugging
                Console.WriteLine($"Nombre d'interventions trouvées : {loanInterventions.Count}");

                var loanReasonIds = loanInterventions.Select(i => i.IdLoanReason ?? 0).Distinct().ToList();
                var loanReasonsDict = _mdBLL.GetAllMdLoanReasons().ElementList
                    .Where(r => loanReasonIds.Contains(r.Id))
                    .ToDictionary(r => r.Id, r => r.Name);

                // Calcul des statistiques
                var numberOfLoansRequested = loanInterventions.Count;
                var numberOfLoansGranted = loanInterventions.Count(i => i.LoanAmount > 0);
                var totalLoanAmount = loanInterventions.Sum(i => i.LoanAmount);
                var remainingBalance = loanInterventions.Sum(i => i.LoanAmountBalance);

                // Debugging
                Console.WriteLine($"Nombre de prêts demandés : {numberOfLoansRequested}");
                Console.WriteLine($"Nombre de prêts accordés : {numberOfLoansGranted}");
                Console.WriteLine($"Somme totale des prêts : {totalLoanAmount}");
                Console.WriteLine($"Solde restant : {remainingBalance}");

                var loanReasons = loanInterventions
                    .GroupBy(i => i.IdLoanReason ?? 0)
                    .Select(group => new LoanReasonViewModel
                    {
                        Reason = loanReasonsDict.ContainsKey(group.Key) ? loanReasonsDict[group.Key] : "Inconnu",
                        Count = group.Count()
                    }).ToList();

                var loansByBank = loanInterventions
                    .Where(i => i.IdClient.HasValue)
                    .GroupBy(i => {
                        var client = _clientBLL.GetClient(i.IdClient.Value).Element;
                        if (client != null && client.IdBank.HasValue)
                        {
                            var bank = _mdBLL.GetMdBank(client.IdBank.Value).Element;
                            return bank != null ? bank.Name : "Inconnu";
                        }
                        return "Inconnu";
                    })
                    .Select(group => new LoanByBankViewModel
                    {
                        Bank = group.Key,
                        Count = group.Count()
                    }).ToList();

                var model = new LoanStatisticsViewModel
                {
                    NumberOfLoansRequested = numberOfLoansRequested,
                    NumberOfLoansGranted = numberOfLoansGranted,
                    TotalLoanAmount = totalLoanAmount,
                    RemainingBalance = remainingBalance,
                    LoanReasons = loanReasons,
                    LoansByBank = loansByBank
                };

                return View(model);
            }
            catch (Exception ex)
            {
                // Log d'erreur
                Console.WriteLine($"Erreur lors de la récupération des statistiques des prêts: {ex.Message}");
                ModelState.AddModelError("", "Une erreur est survenue lors du chargement des statistiques des prêts.");
                return View();
            }
        }

    }
}
