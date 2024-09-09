using BusinessLayer.Communication.Responses.Common;
using CoreLib.Definitions;
using DataAccess.BOL.Client;
using DataAccess.BOL.Employe;
using DataAccess.BOL.Seminar;
using DataAccess.Models;
using System.Collections.Generic;

namespace BusinessLayer.Logic.Interfaces
{
    public interface ISeminarBLL : IBLL
    {
        // Récupère la liste de tous les séminaires
        GetListResponse<SeminarBOL> GetAllSeminars();

        // Récupère un séminaire spécifique par son ID
        GetItemResponse<SeminarBOL> GetSeminarById(int id);

        // Crée un nouveau séminaire avec une liste d'intervenants
        void CreateSeminar(SeminarBOL seminar, List<Employees> intervenants);

        // Met à jour un séminaire
        void UpdateSeminar(SeminarBOL seminar);

        // Supprime un séminaire
        void DeleteSeminar(int id);

        // Récupère la liste des thèmes de séminaire
        GetListResponse<MdSeminarThemes> GetSeminarThemes();

        // Récupère la liste des participants
        GetListResponse<ClientBOL> GetParticipants();

        // Récupère la liste des intervenants
        GetListResponse<EmployeeBOL> GetIntervenants();
    }
}
