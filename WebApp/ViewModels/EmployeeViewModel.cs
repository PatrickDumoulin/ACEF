using System;
using System.ComponentModel.DataAnnotations;
using WebApp.Models;

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

        [Required]
        [StringLength(100, ErrorMessage = "Le {0} doit comporter au moins {2} caractères.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Nouveau mot de passe")]
        public string NewPassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmer le nouveau mot de passe")]
        [Compare("NewPassword", ErrorMessage = "Le nouveau mot de passe et la confirmation du mot de passe ne correspondent pas.")]
        public string ConfirmPassword { get; set; }

        public string Role {  get; set; }



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

        //// Propriété pour l'email
        //[Required(ErrorMessage = "Email is required")]
        //[Display(Name = "Email")]
        //[EmailAddress(ErrorMessage = "Invalid Email Address")] // Validation de l'adresse email
        //public string Email { get; set; }

        public bool HasRoleSelected => IsSuperUser || IsIntervenant || IsAdministrateurCA;

    }
}
