using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DataAccess.BOL.Seminar;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.ViewModels
{
    public class SeminarSearchViewModel
    {
        // Filtre de la date
        [Display(Name = "Filtre de Date")]
        public string DateFilter { get; set; }

        [Display(Name = "Date de Début")]
        [DataType(DataType.Date)]
        public DateTime? StartDate { get; set; }

        [Display(Name = "Date de Fin")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        // Filtre par thème
        [Display(Name = "Thème")]
        public int? IdSeminarTheme { get; set; }

        // Liste déroulante des thèmes
        public IEnumerable<SelectListItem> Themes { get; set; }

        // Filtre par intervenant
        [Display(Name = "Intervenant")]
        public string IntervenantFilter { get; set; }

        // Résultats de la recherche
        public IEnumerable<SeminarBOL> Seminars { get; set; }

        // Pagination
        public int CurrentPage { get; set; } = 1;
        public int TotalPages { get; set; }

        // Pour savoir si on est sur le filtre semaine en cours, mois en cours, ou intervalle
        public bool semaineEnCours => DateFilter == "Semaine en cours";
        public bool moisEnCours => DateFilter == "Mois en cours";
        public bool intervalle => DateFilter == "Intervalle";
    }
}
