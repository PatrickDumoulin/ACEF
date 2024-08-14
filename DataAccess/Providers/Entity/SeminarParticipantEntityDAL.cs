using DataAccess.BOL.InterventionAttachment;
using DataAccess.BOL.SeminarParticipant;
using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Entity
{
    public class SeminarParticipantEntityDAL : AcefEntityDAL, ISeminarParticipantDAL
    {
        public SeminarParticipantEntityDAL() { }
        public SeminarParticipantEntityDAL(AcefEntityDAL externalDal) : base(externalDal) { }

        public SeminarParticipantBOL GetSeminarParticipantById(int id)
        {
            var record = Db.SeminarsParticipants.FirstOrDefault(x => x.Id == id);
            return record != null ? new SeminarParticipantBOL(record) : null;
        }

        public List<SeminarParticipantBOL> GetSeminarParticipantsBySeminarId(int seminarId)
        {
            var records = Db.SeminarsParticipants
                .Where(x => x.IdSeminar == seminarId)
                .ToList();
            return records.Select(record => new SeminarParticipantBOL(record)).ToList();
        }
        public void CreateSeminarParticipant(SeminarParticipantBOL seminarParticipant)
        {
            var entity = new SeminarsParticipants
            {
                IdSeminar = seminarParticipant.IdSeminar,
                IdClient = seminarParticipant.IdClient,
                LastName = seminarParticipant.LastName,
                FirstName = seminarParticipant.FirstName
            };
            Db.SeminarsParticipants.Add(entity);
            Db.SaveChanges();
        }

        public void DeleteSeminarParticipant(int id)
        {
            var seminarParticipant = Db.SeminarsParticipants.FirstOrDefault(x => x.Id == id);
            if (seminarParticipant != null)
            {
                Db.SeminarsParticipants.Remove(seminarParticipant);
                Db.SaveChanges();
            }
        }

        
    }
}
