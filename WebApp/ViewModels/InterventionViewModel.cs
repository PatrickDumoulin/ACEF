using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class InterventionViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Veuillez indiquer si l'intervention est virtuelle ou non.")]
        public bool IsVirtual { get; set; }

        [Required(ErrorMessage = "La date de l'intervention est requise.")]
        [DataType(DataType.Date, ErrorMessage = "La date de l'intervention n'est pas valide.")]
        public DateTime? DateIntervention { get; set; }

        [Required(ErrorMessage = "Veuillez sélectionner un intervenant.")]
        public int? IdEmployee { get; set; }

        public string EmployeeName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Veuillez sélectionner un dossier client.")]
        public int? IdClient { get; set; }

        public string ClientName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Veuillez sélectionner une référence.")]
        public int? IdReferenceType { get; set; }

        public string ReferenceTypeName { get; set; } = string.Empty; // Ajouté pour afficher le nom de la référence

        [Required(ErrorMessage = "Veuillez sélectionner un statut.")]
        public int? IdStatusType { get; set; }

        public string StatusName { get; set; } = string.Empty; // Ajouté pour afficher le nom du statut

        [Required(ErrorMessage = "Veuillez sélectionner une raison de consultation.")]
        public int? IdInterventionType { get; set; }

        public string InterventionTypeName { get; set; } = string.Empty; // Ajouté pour afficher le nom du type d'intervention

        [Required(ErrorMessage = "Veuillez entrer le montant de la dette.")]
        [Range(0, int.MaxValue, ErrorMessage = "Le montant de la dette doit être un nombre positif.")]
        public decimal? DebtAmount { get; set; }

        public int? IdLoanReason { get; set; }

        public string LoanReasonName { get; set; } 

        public bool? IsLoanPaid { get; set; }

        public IEnumerable<SelectListItem> Solutions { get; set; } = new List<SelectListItem>();

        public List<int> SelectedSolutions { get; set; } = new List<int>();

        // Ajouté pour afficher les noms des solutions
        public List<string> SolutionNames { get; set; } = new List<string>();

        public InterventionViewModel()
        {
            Solutions = new List<SelectListItem>();
            SelectedSolutions = new List<int>();
            SolutionNames = new List<string>();
        }
        public decimal? Income { get; set; } // Revenu (dérivé de DebtAmount)
        public int? Age { get; set; } // Âge du client
    }
}
