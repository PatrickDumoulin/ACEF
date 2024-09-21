using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Core.Definitions;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.BOL.InterventionAttachment;
using DataAccess.BOL.SeminarParticipant;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic
{
    /*[MB] - Ce BLL et les méthodes ne sont pas nécessaires. */
    public class SeminarParticipantBLL : AbstractBLL<ISeminarParticipantDAL>, ISeminarParticipantBLL
    {
        public SeminarParticipantBLL() { }
        public SeminarParticipantBLL(ProviderDALTypes dalType) : base(dalType) { }
        public SeminarParticipantBLL(IDAL externalDAL) : base(externalDAL) { }


        public GetItemResponse<SeminarParticipantBOL> GetSeminarParticipantById(int id)
        {
            var seminarParticipant = base.dal.GetSeminarParticipantById(id);
            return new GetItemResponse<SeminarParticipantBOL>(seminarParticipant);
        }

        public GetListResponse<SeminarParticipantBOL> GetSeminarParticipantsBySeminarId(int seminarId)
        {
            var seminarParticipants = base.dal.GetSeminarParticipantsBySeminarId(seminarId);
            return new GetListResponse<SeminarParticipantBOL>(seminarParticipants);
        }
        public void CreateSeminarParticipant(SeminarParticipantBOL seminarParticipant)
        {
            base.dal.CreateSeminarParticipant(seminarParticipant);
        }

        public void DeleteSeminarParticipant(int id)
        {
            base.dal.DeleteSeminarParticipant(id);
        }

        
    }
}
