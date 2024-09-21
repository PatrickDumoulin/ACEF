using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Core.Definitions;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.BOL.Client;
using DataAccess.BOL.Employe;
using DataAccess.BOL.Seminar;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace BusinessLayer.Logic
{
    /*[MB!] - Ce que je réalise c'est que vous semblez faire un BLL (ou presque) par entité de BD. 
        L'idée d'un BLL est de regroupé plusieurs fonctionnalités/méthodes qui sont sous la même thématique. Ainsi dans le projet, on devrait avoir
            ClientBLL
            EmployeeBLL
            InterventionBLL
            AttachmentBLL (Qui gère les deux types de pièces jointes)
            NoteBLL (Qui gère les deux types de notes)
            SeminarBLL
            MDBLL
            EmployeePermissionsBLL
    */

    /*[MB!] - Les séminaires sont un bel exemple d'entités que l'on appel dissociable. Par exemple une facture est composée d'une entête et de lignes. Dans la BD ce sont deux tables différentes,
              mais au niveau logiciel on ne permet pas d'obtenir juste l'entête ou juste les lignes puisque l'information ne fait pas vraiment de sens si elle est dissociée. C'est pareil ici.
              SeminarBOL devrait contenir une liste de SeminarParticipantBOL et une liste de SeminarEmployeeBOL et la gestion de ces listes est faite par le DAL.
                -> Le get va les populer en fonction des différentes valeurs en BD
                -> Le Create va premièrement faire la création du Seminar, ensuite pour toutes les entrées dans vos listes, populer les sous-tables.
                -> Le update n'est pas vraiment applicable pour les sous-tables
                -> Le delete est la seule opération que l'on voudrait permettre directement pour une sous-table (exemple: DeleteSeminarEmployee(int id)) parce qu'il y a la possiblité de retirer un élément de la sous-table sans affecter la table Seminar en tant que tel.
                -> Le delete d'un Seminar serait à l'inverse du Create. On delete les sous-tables et ensuite le Seminar
    */
    public class SeminarBLL : AbstractBLL<ISeminarDAL>, ISeminarBLL
    {
        public SeminarBLL() { }

        public SeminarBLL(ProviderDALTypes dalType) : base(dalType) { }

        public SeminarBLL(IDAL externalDAL) : base(externalDAL) { }

        //[MB] - GetSiminars()
        // Récupérer tous les séminaires
        public GetListResponse<SeminarBOL> GetAllSeminars()
        {
            var seminars = base.dal.GetAllSeminars();
            return new GetListResponse<SeminarBOL>(seminars);
        }

        // Récupérer un séminaire par ID
        public GetItemResponse<SeminarBOL> GetSeminarById(int id)
        {
            var seminar = base.dal.GetSeminarById(id);

            if (seminar != null)
            {
                // Récupérer les participants et intervenants pour ce séminaire
                seminar.Participants = base.dal.GetParticipantsBySeminarId(seminar.Id);
                seminar.Intervenants = base.dal.GetIntervenantsBySeminarId(seminar.Id);
            }

            return new GetItemResponse<SeminarBOL>(seminar);
        }


        // Créer un séminaire avec des intervenants et des participants
        public void CreateSeminar(SeminarBOL seminar, List<Employees> intervenants)
        {
            // Ajouter le séminaire à la base de données
            base.dal.CreateSeminar(seminar);

            // Ajouter les intervenants au séminaire
            foreach (var intervenant in intervenants)
            {
                var seminarEmployee = new SeminarsEmployees
                {
                    IdSeminar = seminar.Id,
                    IdEmployee = intervenant.Id
                };
                base.dal.AddSeminarEmployee(seminarEmployee);
            }

            // Ajouter les participants au séminaire
            foreach (var participant in seminar.Participants)
            {
                var seminarParticipant = new SeminarsParticipants
                {
                    IdSeminar = seminar.Id,
                    IdClient = participant.Id
                };
                base.dal.AddSeminarParticipant(seminarParticipant); // Cette méthode doit exister dans votre DAL
            }
        }

        // Mettre à jour un séminaire
        public void UpdateSeminar(SeminarBOL seminar)
        {
            base.dal.UpdateSeminar(seminar);
        }

        // Supprimer un séminaire
        public void DeleteSeminar(int id)
        {
            var seminar = base.dal.GetSeminarById(id);
            if (seminar != null)
            {
                base.dal.DeleteSeminar(id);
            }
            else
            {
                throw new Exception("Séminaire introuvable !");
            }
        }

        // Récupérer tous les thèmes de séminaire
        public GetListResponse<MdSeminarThemes> GetSeminarThemes()
        {
            var themes = base.dal.GetSeminarThemes().ToList();
            return new GetListResponse<MdSeminarThemes>(themes);
        }

        // Récupérer tous les participants
        public GetListResponse<ClientBOL> GetParticipants()
        {
            var participants = base.dal.GetParticipants();
            return new GetListResponse<ClientBOL>(participants);
        }

        // Récupérer tous les intervenants
        public GetListResponse<EmployeeBOL> GetIntervenants()
        {
            var intervenants = base.dal.GetIntervenants();
            return new GetListResponse<EmployeeBOL>(intervenants);
        }

    }
}
