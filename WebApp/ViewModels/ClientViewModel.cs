using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

        [Display(Name = "Membre")]
        public bool IsMember { get; set; }

        [Display(Name = "Nom de famille")]
        [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s'-]+$", ErrorMessage = "Le Nom de famille ne peut contenir que des lettres, des espaces, des apostrophes et des tirets.")]
        [Required(ErrorMessage = "Le nom de famille est obligatoire.")]
        public string LastName { get; set; }

        [Display(Name = "Prénom")]
        [Required(ErrorMessage = "Le prénom est obligatoire.")]
        [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ\s'-]+$", ErrorMessage = "Le prénom ne peut contenir que des lettres, des espaces, des apostrophes et des tirets.")]
        public string FirstName { get; set; }

        [Display(Name = "Date de naissance")]
        [Required(ErrorMessage = "La date de naissance est obligatoire.")]
        [DateWithinLastCentury]
        public DateTime? Birthdate { get; set; }

        [Display(Name = "Numéro de téléphone")]
        [RegularExpression(@"^(\(\d{3}\)\s|\d{3}[-\s])?\d{3}[-\s]?\d{4}$",
            ErrorMessage = "Format du numéro de téléphone invalide. Utilisez le format (123) 456-7890 ou 123-456-7890.")]
        [Phone(ErrorMessage = "Format du numéro de téléphone invalide.")]
        public string? PhoneNumber { get; set; }

        [Display(Name = "Email")]
        
        [EmailAddress(ErrorMessage = "Format de l'adresse email invalide.")]
        public string? Email { get; set; }

        [Display(Name = "Dénomination de genre")]
        [Required(ErrorMessage = "Le Dénomination de genre est obligatoire.")]
        public int? IdGenderDenomination { get; set; }

        [Display(Name = "Adresse")]
       
        public string? Address { get; set; }

        [Display(Name = "Code postal")]
        
        public string? ZipCode { get; set; }

        [Display(Name = "État civil")]
        public int? IdMaritalStatus { get; set; }

        [Display(Name = "Situation familiale")]
        public int? IdFamilySituation { get; set; }

        [Display(Name = "Adultes à domicile")]
        public int? AdultsAtHome { get; set; }

        [Display(Name = "Enfants à domicile")]
        public int? ChildsAtHome { get; set; }

        [Display(Name = "Type de logement")]
        public int? IdHabitationType { get; set; }

        [Display(Name = "Banque")]
        public int? IdBank { get; set; }

        [Display(Name = "Situation d'emploi")]
        public int? IdEmploymentSituation { get; set; }

        [Display(Name = "Scolarité")]
        public int? IdScholarshipType { get; set; }

        [Display(Name = "Revenu")]
        //[Required(ErrorMessage = "Le revenu est obligatoire.")]
        public string? Income { get; set; }

        public bool? IsLoanPaid { get; set; }

        // Source de revenus
        public IEnumerable<SelectListItem> IncomeType { get; set; } = new List<SelectListItem>();

        public List<int> SelectedIncomeType { get; set; } = new List<int>();

        // Afficher les noms des sources de revenus
        public List<string> IncomeTypeNames { get; set; } = new List<string>();

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }
    }
}
