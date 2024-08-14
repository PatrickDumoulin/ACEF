using CoreLib.Definitions;
using DataAccess.BOL.InterventionAttachment;
using DataAccess.BOL.SeminarParticipant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Interfaces
{
    public interface ISeminarParticipantDAL : IDAL
    {
        SeminarParticipantBOL GetSeminarParticipantById(int id);
        List<SeminarParticipantBOL> GetSeminarParticipantsBySeminarId(int seminarId);
        void CreateSeminarParticipant(SeminarParticipantBOL seminarParticipant);
        void DeleteSeminarParticipant(int id);
    }
}
