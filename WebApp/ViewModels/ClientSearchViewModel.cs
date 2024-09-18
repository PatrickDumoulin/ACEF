using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace WebApp.ViewModels
{
    public class ClientSearchViewModel
    {
        [Display(Name = "Numéro de dossier")]
        public int? Id { get; set; }

        [Display(Name = "Nom")]
        public string LastName { get; set; }

        [Display(Name = "Prénom")]
        public string FirstName { get; set; }

        [Display(Name = "Numéro de téléphone")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Courriel")]
        public string Email { get; set; }


        public bool? isLoanPaid { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }

        public List<ClientViewModel> Clients { get; set; } = new List<ClientViewModel>();
    }
}
