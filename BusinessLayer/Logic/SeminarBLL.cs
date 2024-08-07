using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Core.Definitions;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.BOL.Client;
using DataAccess.BOL.Employe;
using DataAccess.BOL.Seminar;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using System.Collections.Generic;

namespace BusinessLayer.Logic
{
    public class SeminarBLL : AbstractBLL<ISeminarDAL>, ISeminarBLL
    {
        public SeminarBLL() { }
        public SeminarBLL(ProviderDALTypes dalType) : base(dalType) { }
        public SeminarBLL(IDAL externalDAL) : base(externalDAL) { }

        public GetListResponse<SeminarBOL> GetAllSeminars()
        {
            var seminars = base.dal.GetAllSeminars();
            return new GetListResponse<SeminarBOL>(seminars);
        }

        public GetItemResponse<SeminarBOL> GetSeminarById(int id)
        {
            var seminar = base.dal.GetSeminarById(id);
            return new GetItemResponse<SeminarBOL>(seminar);
        }

        public void CreateSeminar(SeminarBOL seminar)
        {
            base.dal.CreateSeminar(seminar);
        }

        public void UpdateSeminar(SeminarBOL seminar)
        {
            base.dal.UpdateSeminar(seminar);
        }

        public void DeleteSeminar(int id)
        {
            base.dal.DeleteSeminar(id);
        }

        public GetListResponse<MdSeminarThemes> GetSeminarThemes()
        {
            var themes = base.dal.GetSeminarThemes().ToList();
            return new GetListResponse<MdSeminarThemes>(themes);
        }        

        public GetListResponse<ClientBOL> GetParticipants()
        {
            var participants = base.dal.GetParticipants();
            return new GetListResponse<ClientBOL>(participants);
        }
    }
}
