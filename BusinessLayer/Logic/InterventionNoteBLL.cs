using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Core.Definitions;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.BOL.InterventionNote;
using DataAccess.BOL.Note;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic
{
    public class InterventionNoteBLL : AbstractBLL<IInterventionNoteDAL>, IInterventionNoteBLL
    {
        public InterventionNoteBLL() { }
        public InterventionNoteBLL(ProviderDALTypes dalType) : base(dalType) { }
        public InterventionNoteBLL(IDAL externalDAL) : base(externalDAL) { }


        public GetItemResponse<InterventionNoteBOL> GetInterventionNoteById(int id)
        {
            InterventionNoteBOL interventionNoteBOL = base.dal.GetInterventionNoteById(id);
            return new GetItemResponse<InterventionNoteBOL>(interventionNoteBOL);
        }

        public GetListResponse<InterventionNoteBOL> GetInterventionNotesByInterventionId(int interventionId)
        {
            List<InterventionNoteBOL> interventionNotesBOL = base.dal.GetInterventionNotesByInterventionId(interventionId);
            return new GetListResponse<InterventionNoteBOL>(interventionNotesBOL);
        }
        public void CreateInterventionNote(InterventionNoteBOL note)
        {
            base.dal.CreateInterventionNote(note);
        }

        public void UpdateInterventionNote(InterventionNoteBOL note)
        {
            base.dal.UpdateInterventionNote(note);
        }

        public void DeleteInterventionNote(int id)
        {
            base.dal.DeleteInterventionNote(id);
        }

        public int GetInterventionNoteCount(int interventionId)
        {
           return GetInterventionNotesByInterventionId(interventionId).ElementList.Count();
        }

        

       
    }
}
