using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class SeminarViewModel
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Date de l'atelier")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DateSeminar { get; set; }

        public SeminarViewModel()
        {
            DateSeminar = DateTime.Now;
        }

        [Required]
        [Display(Name = "Thème de l'atelier")]
        public int IdSeminarTheme { get; set; }

        [Display(Name = "Participants")]
        public List<int> SelectedParticipants { get; set; } = new List<int>();

        [Display(Name = "Commentaire")]
        public string Notes { get; set; }
    }
}
