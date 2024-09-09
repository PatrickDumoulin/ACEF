using DataAccess.BOL.Client;
using DataAccess.BOL.Employe;
using DataAccess.BOL.Seminar;
using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Providers.Entity
{
    public class SeminarEntityDAL : AcefEntityDAL, ISeminarDAL
    {
        // Récupérer tous les séminaires
        public List<SeminarBOL> GetAllSeminars()
        {
            var records = Db.Seminars
                .Include(s => s.SeminarsParticipants)
                    .ThenInclude(sp => sp.IdClientNavigation)
                .Include(s => s.SeminarsEmployees)
                    .ThenInclude(se => se.IdEmployeeNavigation)
                .ToList();

            return records.Select(r => new SeminarBOL
            {
                Id = r.Id,
                DateSeminar = r.DateSeminar,
                IdSeminarTheme = r.IdSeminarTheme,
                Notes = r.Notes,
                Participants = r.SeminarsParticipants.Select(sp => sp.IdClientNavigation).ToList(),
                Intervenants = r.SeminarsEmployees.Select(se => se.IdEmployeeNavigation).ToList()
            }).ToList();
        }



        // Récupérer un séminaire par ID
        public SeminarBOL GetSeminarById(int id)
        {
            var record = Db.Seminars.FirstOrDefault(x => x.Id == id);
            return record != null ? new SeminarBOL(record) : null;
        }

        // Créer un séminaire
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

            // Assigner l'ID du nouveau séminaire à l'objet SeminarBOL
            seminar.Id = newSeminar.Id;
        }

        // Ajouter un intervenant au séminaire
        public void AddSeminarEmployee(SeminarsEmployees seminarEmployee)
        {
            var exists = Db.SeminarsEmployees.Any(se => se.IdSeminar == seminarEmployee.IdSeminar && se.IdEmployee == seminarEmployee.IdEmployee);

            if (!exists)
            {
                Db.SeminarsEmployees.Add(seminarEmployee);
                Db.SaveChanges();
            }
        }

        // Ajouter un participant au séminaire
        public void AddSeminarParticipant(SeminarsParticipants seminarParticipant)
        {
            // Récupérer le client à partir de l'IdClient pour obtenir le nom et le prénom
            var client = Db.Clients.FirstOrDefault(c => c.Id == seminarParticipant.IdClient);

            if (client != null)
            {
                seminarParticipant.LastName = client.LastName;
                seminarParticipant.FirstName = client.FirstName;
            }

            var exists = Db.SeminarsParticipants.Any(sp => sp.IdSeminar == seminarParticipant.IdSeminar && sp.IdClient == seminarParticipant.IdClient);

            if (!exists)
            {
                Db.SeminarsParticipants.Add(seminarParticipant);
                Db.SaveChanges();
            }
        }

        // Mettre à jour un séminaire
        public void UpdateSeminar(SeminarBOL seminar)
        {
            var existingSeminar = Db.Seminars.FirstOrDefault(x => x.Id == seminar.Id);
            if (existingSeminar != null)
            {
                // Mise à jour des informations du séminaire
                existingSeminar.DateSeminar = seminar.DateSeminar;
                existingSeminar.IdSeminarTheme = seminar.IdSeminarTheme;
                existingSeminar.Notes = seminar.Notes;

                // Mise à jour des participants
                var existingParticipants = Db.SeminarsParticipants.Where(sp => sp.IdSeminar == seminar.Id).ToList();
                Db.SeminarsParticipants.RemoveRange(existingParticipants); // Supprime les participants existants
                foreach (var participant in seminar.Participants)
                {
                    var client = Db.Clients.FirstOrDefault(c => c.Id == participant.Id);
                    if (client != null)
                    {
                        var newParticipant = new SeminarsParticipants
                        {
                            IdSeminar = seminar.Id,
                            IdClient = participant.Id,
                            LastName = client.LastName,
                            FirstName = client.FirstName
                        };
                        Db.SeminarsParticipants.Add(newParticipant);
                    }
                }

                // Mise à jour des intervenants
                var existingIntervenants = Db.SeminarsEmployees.Where(se => se.IdSeminar == seminar.Id).ToList();
                Db.SeminarsEmployees.RemoveRange(existingIntervenants); // Supprime les intervenants existants
                foreach (var intervenant in seminar.Intervenants)
                {
                    var newIntervenant = new SeminarsEmployees
                    {
                        IdSeminar = seminar.Id,
                        IdEmployee = intervenant.Id
                    };
                    Db.SeminarsEmployees.Add(newIntervenant);
                }

                Db.SaveChanges();
            }
        }

        // Supprimer un séminaire
        public void DeleteSeminar(int id)
        {
            var seminar = Db.Seminars.FirstOrDefault(x => x.Id == id);
            if (seminar != null)
            {
                // 1. Supprimer les participants associés
                var participants = Db.SeminarsParticipants.Where(sp => sp.IdSeminar == id).ToList();
                if (participants.Any())
                {
                    Db.SeminarsParticipants.RemoveRange(participants);
                }

                // 2. Supprimer les intervenants associés
                var intervenants = Db.SeminarsEmployees.Where(se => se.IdSeminar == id).ToList();
                if (intervenants.Any())
                {
                    Db.SeminarsEmployees.RemoveRange(intervenants);
                }

                // 3. Supprimer le séminaire
                Db.Seminars.Remove(seminar);
                Db.SaveChanges();
            }
        }

        // Récupérer tous les thèmes de séminaire
        public IEnumerable<MdSeminarThemes> GetSeminarThemes()
        {
            return Db.MdSeminarThemes.ToList();
        }

        // Récupérer tous les participants
        public List<ClientBOL> GetParticipants()
        {
            var participants = Db.Clients.Select(c => new ClientBOL(c)).ToList();
            return participants;
        }

        // Récupérer tous les intervenants
        public List<EmployeeBOL> GetIntervenants()
        {
            var intervenants = Db.Employees.Select(c => new EmployeeBOL(c)).ToList();
            return intervenants;
        }
        public List<Clients> GetParticipantsBySeminarId(int seminarId)
        {
            var participants = Db.SeminarsParticipants
                .Where(sp => sp.IdSeminar == seminarId)
                .Select(sp => sp.IdClientNavigation)  // Utilisation de IdClientNavigation pour accéder à l'entité Client
                .ToList();

            return participants;
        }

        public List<Employees> GetIntervenantsBySeminarId(int seminarId)
        {
            var intervenants = Db.SeminarsEmployees
                .Where(se => se.IdSeminar == seminarId)
                .Select(se => se.IdEmployeeNavigation)  // Utilisation de IdEmployeeNavigation pour accéder à l'entité Employee
                .ToList();

            return intervenants;
        }

    }
}
