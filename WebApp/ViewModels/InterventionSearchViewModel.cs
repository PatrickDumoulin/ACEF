using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;

namespace WebApp.ViewModels
{
    public class InterventionSearchViewModel
    {
        public int? Id { get; set; }
        public int? IdClient { get; set; }
        public int? IdEmployee { get; set; }
        public int? IdInterventionType { get; set; }
        


        public string DateFilter { get; set; } // Week, Month, Interval
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool? IsLoanUnpaid { get; set; }

        //Flags Recherche

        public bool semaineEnCours { get; set; } = false;
        public bool moisEnCours { get; set; } = false;
        public bool intervalle { get; set; } = false;

        // Drop-down lists
        public IEnumerable<SelectListItem> Employees { get; set; }
        public IEnumerable<SelectListItem> InterventionTypes { get; set; }

        // The list of interventions to display
        public IEnumerable<InterventionViewModel> Interventions { get; set; }
    }
}
