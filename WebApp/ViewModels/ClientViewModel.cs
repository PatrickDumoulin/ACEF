using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
	public class ClientViewModel
	{
        public int Id { get; set; }
        public string FullName { get; set; }
        [Display(Name = "Membre")]
        public bool IsMember { get; set; }
        [Display(Name = "Nom de famille")]
        public string LastName { get; set; }
        [Display(Name = "Prénom")]
        public string FirstName { get; set; }
        [Display(Name = "Date de naissance")]
        public DateTime? Birthdate { get; set; }
        [Display(Name = "Numéro de téléphone")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Email")]
        public string Email { get; set; }
        [Display(Name = "Dénomination de genre")]
        public int? IdGenderDenomination { get; set; }
        [Display(Name = "Adresse")]
        public string Address { get; set; }
        [Display(Name = "Code postal")]
        public string ZipCode { get; set; }
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
        [Display(Name = "Type de bourse")]
        public int? IdScholarshipType { get; set; }
        [Display(Name = "Revenu")]
        public string Income { get; set; }
        public bool? IsLoanPaid { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }

        //public IEnumerable<SelectListItem> MaritalStatus { get; set; }
        //public IEnumerable<SelectListItem> FamilySituation { get; set; }
        //public IEnumerable<SelectListItem> HomeType { get; set; }
    }
}
