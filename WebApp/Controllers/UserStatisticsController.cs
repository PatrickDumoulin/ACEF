using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApp.ViewModels;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Injection;
using DataModels.BOL.Client;
using DataAccess.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Superutilisateur, AdministrateurCA")]
    public class UserStatisticsController : Controller
    {
        private readonly IClientBLL _clientBLL;
        private readonly IClientIncomeTypeBLL _clientIncomeTypeBLL;
        private readonly IInterventionBLL _interventionBLL;
        private readonly IMDBLL _mdBLL;

        public UserStatisticsController() : base()
        {
            _clientBLL = Injector.ImplementBll<IClientBLL>();
            _mdBLL = Injector.ImplementBll<IMDBLL>();
            _clientIncomeTypeBLL = Injector.ImplementBll<IClientIncomeTypeBLL>();
            _interventionBLL = Injector.ImplementBll<IInterventionBLL>();
        }

        public ActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            // Par défaut, si aucune date n'est fournie, on prend les 30 derniers jours
            if (!startDate.HasValue || !endDate.HasValue)
            {
                startDate = DateTime.Today.AddDays(-30);
                endDate = DateTime.Today;
            }

            // Pas besoin d'utiliser ElementList ici
            var interventions = _interventionBLL.GetInterventionsByDateRange(startDate.Value, endDate.Value);
            var distinctClientIds = interventions.Select(i => i.IdClient).Distinct().ToList();
            var clients = _clientBLL.GetClients().ElementList.Where(c => distinctClientIds.Contains(c.Id)).ToList();
            var clientIncomeTypes = clients.SelectMany(c => _clientIncomeTypeBLL.GetClientIncomeTypesByClientId(c.Id).ElementList).ToList();
            var interventionTypes = _mdBLL.GetAllMdInterventionTypes().ElementList;

            // Calcul des statistiques
            var genderStats = clients.Where(c => c.IdGenderDenomination.HasValue)
                          .GroupBy(c => c.IdGenderDenomination.Value)
                          .Select(g => new StatisticViewModel
                          {
                              Label = _mdBLL.GetMdGenderDenomination(g.Key)?.Element?.Name ?? "Inconnu",
                              Total = g.Count()
                          }).ToList();

            var ageStats = clients.GroupBy(c => GetAgeCategory(c.Birthdate))
                       .Select(g => new StatisticViewModel
                       {
                           Label = g.Key,
                           Total = g.Count()
                       }).ToList();

            var familySituationStats = clients.Where(c => c.IdFamilySituation.HasValue)
                                   .GroupBy(c => c.IdFamilySituation.Value)
                                   .Select(g => new StatisticViewModel
                                   {
                                       Label = _mdBLL.GetMdFamilySituation(g.Key)?.Element?.Name ?? "Inconnu",
                                       Total = g.Count()
                                   }).ToList();

            var incomeSourceStats = clientIncomeTypes
                                    .GroupBy(c => c.IdIncomeType)
                                    .Select(g => new StatisticViewModel
                                    {
                                        Label = _mdBLL.GetMdIncomeType(g.Key)?.Element?.Name ?? "Inconnu",
                                        Total = g.Count()
                                    }).ToList();

            var netIncomeStats = clients.GroupBy(c => GetIncomeCategory(c.Income))
                                        .Select(g => new StatisticViewModel
                                        {
                                            Label = g.Key,
                                            Total = g.Count()
                                        }).ToList();

            var participationStats = interventions.Where(i => i.IdInterventionType.HasValue)
                                    .GroupBy(i => i.IdInterventionType.Value)
                                    .Select(g => new StatisticViewModel
                                    {
                                        Label = interventionTypes.FirstOrDefault(it => it.Id == g.Key)?.Name ?? "Inconnu",
                                        Total = g.Count()
                                    }).ToList();

            var model = new UserStatisticsViewModel
            {
                StartDate = startDate.Value,
                EndDate = endDate.Value,
                GenderStatistics = genderStats,
                AgeStatistics = ageStats,
                FamilySituationStatistics = familySituationStats,
                IncomeSourceStatistics = incomeSourceStats,
                NetIncomeStatistics = netIncomeStats,
                ParticipationStatistics = participationStats
            };

            return View("/Views/Statistics/Index.cshtml", model);
        }


        private string GetAgeCategory(DateTime? birthdate)
        {
            if (birthdate == null) return "Inconnu";
            var age = DateTime.Today.Year - birthdate.Value.Year;
            if (age < 18) return "Moins de 18 ans";
            if (age <= 27) return "18 - 27 ans";
            if (age <= 37) return "28 - 37 ans";
            if (age <= 47) return "38 - 47 ans";
            if (age <= 57) return "48 - 57 ans";
            if (age <= 67) return "58 - 67 ans";
            if (age <= 74) return "68 - 74 ans";
            return "75 ans et plus";
        }

        private string GetIncomeCategory(byte[] income)
        {
            if (income == null) return "Inconnu";
            var netIncome = BitConverter.ToInt32(income, 0);
            if (netIncome < 10000) return "Moins de 10 000 $";
            if (netIncome <= 14999) return "10 000 $ - 14 999 $";
            if (netIncome <= 19999) return "15 000 $ - 19 999 $";
            if (netIncome <= 24999) return "20 000 $ - 24 999 $";
            if (netIncome <= 29999) return "25 000 $ - 29 999 $";
            if (netIncome <= 34999) return "30 000 $ - 34 999 $";
            if (netIncome <= 39999) return "35 000 $ - 39 999 $";
            if (netIncome <= 44999) return "40 000 $ - 44 999 $";
            if (netIncome <= 49999) return "45 000 $ - 49 999 $";
            if (netIncome <= 54999) return "50 000 $ - 54 999 $";
            if (netIncome <= 59999) return "55 000 $ - 59 999 $";
            return "60 000 $ et plus";
        }
    }
}
