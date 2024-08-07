using DataAccess.BOL.Note;
using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Providers.Entity
{
    public class NoteEntityDAL : AcefEntityDAL, INoteDAL
    {
        public NoteEntityDAL() { }
        public NoteEntityDAL(AcefEntityDAL externalDal) : base(externalDal) { }

        public NoteBOL GetNoteById(int id)
        {
            var record = Db.ClientsNotes.FirstOrDefault(x => x.Id == id);
            return record != null ? MapToBOL(record) : null;
        }

        public List<NoteBOL> GetNotesByClientId(int clientId)
        {
            var records = Db.ClientsNotes
                .Where(x => x.IdClient == clientId)
                .OrderByDescending(x => x.CreatedDate)
                .ToList();
            return records.Select(MapToBOL).ToList();
        }

        public void CreateNote(NoteBOL note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note), "Note cannot be null");
            }

            var newNote = MapToEntity(note);
            newNote.CreatedDate = DateTime.Now;
            Db.ClientsNotes.Add(newNote);
            Db.SaveChanges();
        }

        public void UpdateNote(NoteBOL note)
        {
            if (note == null)
            {
                throw new ArgumentNullException(nameof(note), "Note cannot be null");
            }

            var existingNote = Db.ClientsNotes.FirstOrDefault(x => x.Id == note.Id);

            if (existingNote == null)
            {
                throw new KeyNotFoundException($"Note with ID {note.Id} not found.");
            }

            existingNote.Comment = note.Comment;
            existingNote.IdEmployee = note.IdEmployee;
            existingNote.IdClient = note.IdClient;
            existingNote.CreatedDate = note.CreatedDate;

            Db.SaveChanges();
        }

        public void DeleteNote(int id)
        {
            var note = Db.ClientsNotes.FirstOrDefault(x => x.Id == id);
            if (note != null)
            {
                Db.ClientsNotes.Remove(note);
                Db.SaveChanges();
            }
        }

        private NoteBOL MapToBOL(ClientsNotes entity)
        {
            return new NoteBOL
            {
                Id = entity.Id,
                IdClient = entity.IdClient,
                IdEmployee = entity.IdEmployee,
                Comment = entity.Comment,
                CreatedDate = entity.CreatedDate
            };
        }

        private ClientsNotes MapToEntity(NoteBOL bol)
        {
            return new ClientsNotes
            {
                Id = bol.Id,
                IdClient = bol.IdClient,
                IdEmployee = bol.IdEmployee,
                Comment = bol.Comment,
                CreatedDate = bol.CreatedDate
            };
        }
    }
}
