using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Core.Definitions;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.BOL.Note;
using DataAccess.Providers.Interfaces;
using System.Collections.Generic;

namespace BusinessLayer.Logic
{
    public class NoteBLL : AbstractBLL<INoteDAL>, INoteBLL
    {
        public NoteBLL() { }
        public NoteBLL(ProviderDALTypes dalType) : base(dalType) { }
        public NoteBLL(IDAL externalDAL) : base(externalDAL) { }

        //[MB] - Renommer pour GetNote(int id)
        public GetItemResponse<NoteBOL> GetNoteById(int id)
        {
            NoteBOL noteBOL = base.dal.GetNoteById(id);
            return new GetItemResponse<NoteBOL>(noteBOL);
        }

        //[MB] - Renommer pour GetNotes(int clientId)
        public GetListResponse<NoteBOL> GetNotesByClientId(int clientId)
        {
            List<NoteBOL> notesBOL = base.dal.GetNotesByClientId(clientId);
            return new GetListResponse<NoteBOL>(notesBOL);
        }

        //[MB] - Doit avoir un type de retour pour savoir si ça fonctionne et passe les validations
        public void CreateNote(NoteBOL noteBOL)
        {
            //[MB] - Validations?
            base.dal.CreateNote(noteBOL);
        }

        //[MB] - Doit avoir un type de retour pour savoir si ça fonctionne et passe les validations
        public void UpdateNote(NoteBOL noteBOL)
        {
            //[MB] - Validations?
            base.dal.UpdateNote(noteBOL);
        }

        //[MB] - Doit avoir un type de retour pour savoir si ça fonctionne
        public void DeleteNote(int id)
        {
            base.dal.DeleteNote(id);
        }
    }
}
