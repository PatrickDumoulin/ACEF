using CoreLib.Definitions;
using DataAccess.BOL.InterventionNote;
using DataAccess.BOL.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Interfaces
{
    public interface IInterventionNoteDAL : IDAL
    {
        InterventionNoteBOL GetInterventionNoteById(int id);
        List<InterventionNoteBOL> GetInterventionNotesByInterventionId(int interventionId);
        void CreateInterventionNote(InterventionNoteBOL note);
        void UpdateInterventionNote(InterventionNoteBOL note);
        void DeleteInterventionNote(int id);
    }
}
