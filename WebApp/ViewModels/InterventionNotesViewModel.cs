using DataAccess.BOL.InterventionNote;
using DataAccess.BOL.Note;

namespace WebApp.ViewModels
{
    public class InterventionNotesViewModel
    {
        public InterventionViewModel Intervention { get; set; }
        public IEnumerable<InterventionNoteBOL> InterventionNotes { get; set; }

        
    }
}
