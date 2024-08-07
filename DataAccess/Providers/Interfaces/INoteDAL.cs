using CoreLib.Definitions;
using DataAccess.BOL.Note;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Interfaces
{
    public interface INoteDAL : IDAL
    {
        NoteBOL GetNoteById(int id);
        List<NoteBOL> GetNotesByClientId(int clientId);
        void CreateNote(NoteBOL note);
        void UpdateNote(NoteBOL note);
        void DeleteNote(int id);
    }
}
