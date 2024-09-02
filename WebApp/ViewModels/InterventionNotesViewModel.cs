using BusinessLayer.Logic.Interfaces;
using DataAccess.BOL.InterventionNote;
using DataAccess.BOL.Note;

namespace WebApp.ViewModels
{
    public class InterventionNotesViewModel : BaseViewModel
    {

        public InterventionNotesViewModel(IEmployeeBLL employeeBLL) : base(employeeBLL)
        {
            
        }

        public InterventionViewModel Intervention { get; set; }
        public IEnumerable<InterventionNoteBOL> InterventionNotes { get; set; }
        public string CreatedBy { get; set; }

        

        public string EmployeeName { get; set; }

        // Propriétés pour la pagination
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }


    }
}
