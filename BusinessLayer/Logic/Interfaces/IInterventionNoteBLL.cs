using BusinessLayer.Communication.Responses.Common;
using CoreLib.Definitions;
using DataAccess.BOL.InterventionNote;
using DataAccess.BOL.Note;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic.Interfaces
{
    public interface IInterventionNoteBLL : IBLL
    {
        GetItemResponse<InterventionNoteBOL> GetInterventionNoteById(int id);
        GetListResponse<InterventionNoteBOL> GetInterventionNotesByInterventionId(int interventionId);
        void CreateInterventionNote(InterventionNoteBOL note);
        void UpdateInterventionNote(InterventionNoteBOL note);
        void DeleteInterventionNote(int id);
    }
}
