using BusinessLayer.Logic.Interfaces;
using CoreLib.Injection;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class RepartitionController : Controller
    {
        private readonly IInterventionBLL _interventionBLL;
        private readonly IMDBLL _mdBLL;
        private readonly IClientBLL _clientBLL;

        public RepartitionController()
        {
            _interventionBLL = Injector.ImplementBll<IInterventionBLL>();
            _mdBLL = Injector.ImplementBll<IMDBLL>();
            _clientBLL = Injector.ImplementBll<IClientBLL>();
        }

        public ActionResult Index(DateTime? startDate, DateTime? endDate)
        {
            try
            {
                // Si les dates ne sont pas fournies, définir une plage par défaut
                if (!startDate.HasValue || !endDate.HasValue)
                {
                    startDate = DateTime.MinValue;
                    endDate = DateTime.MaxValue;
                }

                // Vérification des dates
                if (startDate > endDate)
                {
                    ModelState.AddModelError("", "La date de début ne peut pas être postérieure à la date de fin.");
                    return View();
                }

                // Récupérer toutes les interventions dans la plage de dates
                var response = _interventionBLL.GetInterventions();
                var interventionsInRange = response.ElementList
                    .Where(i => i.DateIntervention >= startDate && i.DateIntervention <= endDate)
                    .ToList();

                // Provenance par Référence
                var referenceSources = _mdBLL.GetAllMdReferenceSources().ElementList;
                var referenceGroup = interventionsInRange
                    .GroupBy(i => i.IdReferenceType ?? 0)
                    .Select(group => new ReferenceDistributionViewModel
                    {
                        ReferenceName = referenceSources.FirstOrDefault(r => r.Id == group.Key)?.Name ?? "Inconnu",
                        Count = group.Count()
                    }).ToList();

                // Récupérer la liste des banques
                var banks = _mdBLL.GetAllMdBanks().ElementList;

                // Extraire les banques pour "Desjardins"
                var desjardinsInterventions = interventionsInRange
                    .Where(i => i.IdReferenceType.HasValue && referenceSources.Any(r => r.Id == i.IdReferenceType.Value && r.Name.Contains("Desjardins")))
                    .ToList();

                // Filtrer les banques Desjardins
                var desjardinsDetails = desjardinsInterventions
                    .Where(i => i.IdClient.HasValue)
                    .GroupBy(i => {
                        var client = _clientBLL.GetClient(i.IdClient.Value).Element;
                        if (client != null && client.IdBank.HasValue)
                        {
                            var bank = banks.FirstOrDefault(b => b.Id == client.IdBank && b.IsDesjardins);
                            return bank != null ? bank.Name : "Inconnu";
                        }
                        return "Inconnu";
                    })
                    .Select(group => new DesjardinsDetailViewModel
                    {
                        BankName = group.Key,
                        Count = group.Count()
                    }).Where(d => d.BankName != "Inconnu").ToList();

                // Usager par Caisse
                var clientsWithInterventions = interventionsInRange
                    .Where(i => i.IdClient.HasValue)
                    .Select(i => i.IdClient.Value)
                    .Distinct()
                    .ToList();

                var clients = clientsWithInterventions
                    .Select(clientId => _clientBLL.GetClient(clientId).Element)
                    .Where(client => client != null)
                    .ToList();

                var clientsByBank = clients
                    .GroupBy(client => client.IdBank ?? 0)
                    .Select(group => new ClientByBankViewModel
                    {
                        BankName = banks.FirstOrDefault(b => b.Id == group.Key)?.Name ?? "Inconnu",
                        Count = group.Count()
                    }).ToList();

                var model = new RepartitionViewModel
                {
                    ReferenceDistribution = referenceGroup,
                    DesjardinsDetails = desjardinsDetails,
                    ClientsByBank = clientsByBank
                };

                return View(model);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de la génération des répartitions : {ex.Message}");
                ModelState.AddModelError("", "Une erreur est survenue lors de la génération des répartitions.");
                return View();
            }
        }
    }
}
