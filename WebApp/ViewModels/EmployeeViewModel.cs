using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le prénom est requis.")]
        [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ '-]+$", ErrorMessage = "Le prénom ne peut contenir que des lettres, des espaces, des apostrophes et des tirets.\".")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Le nom est requis.")]
        [RegularExpression(@"^[A-Za-zÀ-ÖØ-öø-ÿ '-]+$", ErrorMessage = "Le Nom de famille ne peut contenir que des lettres, des espaces, des apostrophes et des tirets.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

       
        [Display(Name = "Password")]
        [DataType(DataType.Password)] // Indication que c'est un mot de passe
        public string PasswordHash { get; set; }

        [Display(Name = "Nouveau mot de passe")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }



        [Display(Name = "Last Login Date")]
        public DateTime? LastLoginDate { get; set; }

        [Display(Name = "Active")]
        public bool? Active { get; set; }

        // Propriétés pour les rôles
        [Display(Name = "Superutilisateur")]
        public bool IsSuperUser { get; set; }

        [Display(Name = "Intervenant")]
        public bool IsIntervenant { get; set; }

        [Display(Name = "Administrateur CA")]
        public bool IsAdministrateurCA { get; set; }

        //Pagination
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        // Propriété pour l'email
        [Required(ErrorMessage = "Email is required")]
        [Display(Name = "Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")] // Validation de l'adresse email
        public string Email { get; set; }
    }
}
