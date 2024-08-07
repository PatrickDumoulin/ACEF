using BusinessLayer.Communication.Responses.Common;
using CoreLib.Definitions;
using DataAccess.BOL.Note;
using DataAccess.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLayer.Logic.Interfaces
{
    public interface INoteBLL : IBLL
    {
        GetItemResponse<NoteBOL> GetNoteById(int id);
        GetListResponse<NoteBOL> GetNotesByClientId(int clientId);
        void CreateNote(NoteBOL note);
        void UpdateNote(NoteBOL note);
        void DeleteNote(int id);
    }
}
