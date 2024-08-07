using DataAccess.BOL.Client;
using DataAccess.BOL.Employe;
using DataAccess.BOL.Seminar;
using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Providers.Entity
{
    public class SeminarEntityDAL : AcefEntityDAL, ISeminarDAL
    {
        public List<SeminarBOL> GetAllSeminars()
        {
            var records = Db.Seminars.ToList();
            return records.Select(r => new SeminarBOL(r)).ToList();
        }

        public SeminarBOL GetSeminarById(int id)
        {
            var record = Db.Seminars.FirstOrDefault(x => x.Id == id);
            return record != null ? new SeminarBOL(record) : null;
        }

        public void CreateSeminar(SeminarBOL seminar)
        {
            var newSeminar = new Seminars
            {
                DateSeminar = seminar.DateSeminar,
                IdSeminarTheme = seminar.IdSeminarTheme,
                Notes = seminar.Notes,
                CreatedDate = seminar.CreatedDate
            };

            Db.Seminars.Add(newSeminar);
            Db.SaveChanges();
        }

        public void UpdateSeminar(SeminarBOL seminar)
        {
            var existingSeminar = Db.Seminars.FirstOrDefault(x => x.Id == seminar.Id);
            if (existingSeminar != null)
            {
                existingSeminar.DateSeminar = seminar.DateSeminar;
                existingSeminar.IdSeminarTheme = seminar.IdSeminarTheme;
                existingSeminar.Notes = seminar.Notes;
                Db.SaveChanges();
            }
        }

        public void DeleteSeminar(int id)
        {
            var seminar = Db.Seminars.FirstOrDefault(x => x.Id == id);
            if (seminar != null)
            {
                Db.Seminars.Remove(seminar);
                Db.SaveChanges();
            }
        }

        public IEnumerable<MdSeminarThemes> GetSeminarThemes()
        {
            return Db.MdSeminarThemes.ToList();
        }

        public List<ClientBOL> GetParticipants()
        {
            var participants = Db.Clients.Select(c => new ClientBOL(c)).ToList();
            return participants;
        }

        
    }
}
