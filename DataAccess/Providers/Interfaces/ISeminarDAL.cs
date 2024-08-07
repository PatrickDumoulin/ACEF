using CoreLib.Definitions;
using DataAccess.BOL.Client;
using DataAccess.BOL.Employe;
using DataAccess.BOL.Seminar;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Interfaces
{
    public interface ISeminarDAL : IDAL
    {
        List<SeminarBOL> GetAllSeminars();
        SeminarBOL GetSeminarById(int id);
        void CreateSeminar(SeminarBOL seminar);
        void UpdateSeminar(SeminarBOL seminar);
        void DeleteSeminar(int id);
        IEnumerable<MdSeminarThemes> GetSeminarThemes();
        
        List<ClientBOL> GetParticipants();
    }
}
