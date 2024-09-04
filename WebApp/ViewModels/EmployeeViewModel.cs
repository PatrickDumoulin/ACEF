using System;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "First Name is required")]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required")]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Username is required")]
        [Display(Name = "Username")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required")]
        [Display(Name = "Password")]
        [DataType(DataType.Password)] // Indication que c'est un mot de passe
        public string PasswordHash { get; set; }

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

        //[Required(ErrorMessage = "Email is required")]
        //[Display(Name = "Email")]
        //public string Email { get; set; }
    }
}
