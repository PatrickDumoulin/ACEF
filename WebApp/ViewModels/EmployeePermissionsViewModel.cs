using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class EmployeePermissionsViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Employé")]
        public int? EmployeeId { get; set; }

        [Display(Name = "Autoriser les Interventions")]
        public bool AllowInterventions { get; set; }

        [Display(Name = "Autoriser les Rapports")]
        public bool AllowReports { get; set; }

        [Display(Name = "Autoriser Superutilisateur")]
        public bool AllowSu { get; set; }

        public SelectList Employees { get; set; }
    }
}
