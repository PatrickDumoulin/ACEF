using DataAccess.BOL.InterventionNote;
using DataAccess.BOL.Note;
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
    public class InterventionNoteEntityDAL : AcefEntityDAL, IInterventionNoteDAL
    {
        public InterventionNoteEntityDAL() { }
        public InterventionNoteEntityDAL(AcefEntityDAL externalDal) : base(externalDal) { }

        public InterventionNoteBOL GetInterventionNoteById(int id)
        {
            var record = Db.InterventionsNotes.FirstOrDefault(x => x.Id == id);
            return record != null ? MapToBOL(record) : null;
        }

        public List<InterventionNoteBOL> GetInterventionNotesByInterventionId(int interventionId)
        {
            var records = Db.InterventionsNotes
                .Where(x => x.IdIntervention == interventionId)
                .OrderByDescending(x => x.CreatedDate)
                .ToList();
            return records.Select(MapToBOL).ToList();
        }
        public void CreateInterventionNote(InterventionNoteBOL note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note), "InterventionNote cannot be null");
            }

            var newNote = MapToEntity(note);
            newNote.CreatedDate = DateTime.Now;
            Db.InterventionsNotes.Add(newNote);
            Db.SaveChanges();
        }

        public void UpdateInterventionNote(InterventionNoteBOL note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note), "Note cannot be null");
            }

            var existingNote = Db.InterventionsNotes.FirstOrDefault(x => x.Id == note.Id);

            if (existingNote == null)
            {
                throw new KeyNotFoundException($"Note with ID {note.Id} not found.");
            }

            existingNote.Comment = note.Comment;
            existingNote.IdEmployee = note.IdEmployee;
            existingNote.IdIntervention = note.IdIntervention;
            existingNote.CreatedDate = note.CreatedDate;

            Db.SaveChanges();
        }

        public void DeleteInterventionNote(int id)
        {
            var note = Db.InterventionsNotes.FirstOrDefault(x => x.Id == id);
            if (note != null)
            {
                Db.InterventionsNotes.Remove(note);
                Db.SaveChanges();
            }
        }

        

        private InterventionNoteBOL MapToBOL(InterventionsNotes entity)
        {
            return new InterventionNoteBOL
            {

                IdIntervention = entity.IdIntervention,
                IdEmployee = entity.IdEmployee,
                Comment = entity.Comment,
                CreatedDate = entity.CreatedDate
            };
        }

        private InterventionsNotes MapToEntity(InterventionNoteBOL bol)
        {
            return new InterventionsNotes
            {
                
                IdIntervention = bol.IdIntervention,
                IdEmployee = bol.IdEmployee,
                Comment = bol.Comment,
                CreatedDate = bol.CreatedDate
            };
        }
    }
}
