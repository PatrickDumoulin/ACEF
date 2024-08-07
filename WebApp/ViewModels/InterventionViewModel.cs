using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class InterventionViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Est virtuel")]
        public bool IsVirtual { get; set; }

        [Display(Name = "Date de l'intervention")]
        public DateTime? DateIntervention { get; set; }

        [Display(Name = "Intervenant")]
        public int? IdEmployee { get; set; }

        [Display(Name = "# Dossier client")]
        public int? IdClient { get; set; }

        [Display(Name = "Réference")]
        public int? IdReferenceType { get; set; }

        [Display(Name = "Statut")]
        public int? IdStatusType { get; set; }

        [Display(Name = "Raison consultation")]
        public int? IdInterventionType { get; set; }

        [Display(Name = "Montant de la dette")]
        public byte[] DebtAmount { get; set; }

        [Display(Name = "But d'emprunt petit prêt")]
        public int? IdLoanReason { get; set; }

        [Display(Name = "Petit prêt remboursé")]
        public bool? IsLoanPaid { get; set; }

        [Display(Name = "Petit prêt remboursé")]
        public int? IdInterventionSolution { get; set; }




        public IEnumerable<SelectListItem> Employees { get; set; }
    }
}
