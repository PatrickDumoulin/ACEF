using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class EmployeeViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }

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
        public string PasswordHash { get; set; }

        [Display(Name = "Last Login Date")]
        public DateTime? LastLoginDate { get; set; }

        [Display(Name = "Active")]
        public bool? Active { get; set; }
    }
}
