using System;
using System.Collections.Generic;

namespace WebApp.ViewModels
{
    public class InterventionReportViewModel
    {
        // Plage de dates pour filtrer les interventions
        public DateTime StartDate { get; set; } = DateTime.Now.AddMonths(-1); // Valeur par défaut
        public DateTime EndDate { get; set; } = DateTime.Now; // Valeur par défaut

        // Filtre pour les revenus
        public int MinIncome { get; set; } = 10000; // Valeur par défaut
        public int MaxIncome { get; set; } = 60000; // Valeur par défaut
        public int IncomeInterval { get; set; } = 5000; // Intervalle par défaut pour le revenu

        // Filtre pour les tranches d'âge
        public int AgeMin { get; set; } = 18; // Valeur par défaut
        public int AgeMax { get; set; } = 75; // Valeur par défaut
        public int AgeInterval { get; set; } = 10; // Intervalle par défaut pour l'âge

        // Liste des interventions à afficher
        public List<InterventionViewModel> Interventions { get; set; } = new List<InterventionViewModel>();
        // Propriétés supplémentaires pour les rapports
        
    }
}
