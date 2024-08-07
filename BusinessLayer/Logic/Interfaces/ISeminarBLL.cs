using BusinessLayer.Communication.Responses.Common;
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

namespace BusinessLayer.Logic.Interfaces
{
    public interface ISeminarBLL : IBLL
    {
        GetListResponse<SeminarBOL> GetAllSeminars();
        GetItemResponse<SeminarBOL> GetSeminarById(int id);
        void CreateSeminar(SeminarBOL seminar);
        void UpdateSeminar(SeminarBOL seminar);
        void DeleteSeminar(int id);
        GetListResponse<MdSeminarThemes> GetSeminarThemes();        
        GetListResponse<ClientBOL> GetParticipants();

    }
}
