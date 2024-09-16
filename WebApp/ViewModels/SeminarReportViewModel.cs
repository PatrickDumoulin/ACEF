using System.ComponentModel.DataAnnotations;

namespace WebApp.ViewModels
{
    public class SeminarReportViewModel
    {
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Now.AddMonths(-1); // Valeur par défaut

        [DataType(DataType.Date)]
        public DateTime EndDate { get; set; } = DateTime.Now; // Valeur par défaut

        [Display(Name = "Thème du séminaire")]
        public int IdSeminarTheme { get; set; }

        // Liste des lignes du rapport
        public List<SeminarReportRow> ReportRows { get; set; } = new List<SeminarReportRow>();
    }
    public class SeminarReportRow
    {
                  
        public string Theme { get; set; }
        public int ParticipantCount { get; set; }
        public int SeminarCount { get; set; }
    }
}
