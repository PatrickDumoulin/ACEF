using BusinessLayer.Communication.Responses.Common;
using CoreLib.Definitions;
using DataAccess.BOL.InterventionAttachment;
using DataAccess.BOL.SeminarParticipant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic.Interfaces
{
    public interface ISeminarParticipantBLL : IBLL
    {
        GetItemResponse<SeminarParticipantBOL> GetSeminarParticipantById(int id);
        GetListResponse<SeminarParticipantBOL> GetSeminarParticipantsBySeminarId(int seminarId);
        void CreateSeminarParticipant(SeminarParticipantBOL seminarParticipant);
        void DeleteSeminarParticipant(int id);
    }
}
