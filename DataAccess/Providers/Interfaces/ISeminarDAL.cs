using CoreLib.Definitions;
using DataAccess.BOL.Client;
using DataAccess.BOL.Employe;
using DataAccess.BOL.Seminar;
using DataAccess.Models;
using System.Collections.Generic;

namespace DataAccess.Providers.Interfaces
{
    public interface ISeminarDAL : IDAL
    {
        // Récupérer tous les séminaires
        List<SeminarBOL> GetAllSeminars();

        // Récupérer un séminaire par ID
        SeminarBOL GetSeminarById(int id);

        // Créer un séminaire
        void CreateSeminar(SeminarBOL seminar);

        // Ajouter un intervenant à un séminaire
        void AddSeminarEmployee(SeminarsEmployees seminarEmployee);

        // Ajouter un participant à un séminaire
        void AddSeminarParticipant(SeminarsParticipants seminarParticipant); // Ajout de cette méthode pour les participants

        // Mettre à jour un séminaire
        void UpdateSeminar(SeminarBOL seminar);

        // Supprimer un séminaire
        void DeleteSeminar(int id);

        // Récupérer tous les thèmes de séminaire
        IEnumerable<MdSeminarThemes> GetSeminarThemes();

        // Récupérer tous les participants
        List<ClientBOL> GetParticipants();

        // Récupérer tous les intervenants
        List<EmployeeBOL> GetIntervenants();
        List<Clients> GetParticipantsBySeminarId(int seminarId);
        public List<Employees> GetIntervenantsBySeminarId(int seminarId);
    }
}
