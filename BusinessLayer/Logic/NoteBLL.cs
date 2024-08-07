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

        public GetItemResponse<NoteBOL> GetNoteById(int id)
        {
            NoteBOL noteBOL = base.dal.GetNoteById(id);
            return new GetItemResponse<NoteBOL>(noteBOL);
        }

        public GetListResponse<NoteBOL> GetNotesByClientId(int clientId)
        {
            List<NoteBOL> notesBOL = base.dal.GetNotesByClientId(clientId);
            return new GetListResponse<NoteBOL>(notesBOL);
        }

        public void CreateNote(NoteBOL noteBOL)
        {
            base.dal.CreateNote(noteBOL);
        }

        public void UpdateNote(NoteBOL noteBOL)
        {
            base.dal.UpdateNote(noteBOL);
        }

        public void DeleteNote(int id)
        {
            base.dal.DeleteNote(id);
        }
    }
}
