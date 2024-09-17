using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApp.Models;

namespace WebApp.ViewModels
{
    public class SeminarViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "La date de l'atelier est obligatoire.")]
        [Display(Name = "Date de l'atelier")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DateWithinLastCentury] // Ajout de l'attribut personnalisé
        public DateTime DateSeminar { get; set; }

        public SeminarViewModel()
        {
            DateSeminar = DateTime.Now;
        }

        [Required]
        [Display(Name = "Thème de l'atelier")]
        public int IdSeminarTheme { get; set; }

        [Display(Name = "Întervenants")]
        public List<int> SelectedIntervenants { get; set; } = new List<int>();

        [Display(Name = "Participants")]
        public List<int> SelectedParticipants { get; set; } = new List<int>();

        [Display(Name = "Commentaire")]
        public string? Notes { get; set; }
    }
}
