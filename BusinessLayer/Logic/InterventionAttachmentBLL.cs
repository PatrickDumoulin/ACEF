using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Core.Definitions;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.BOL.ClientAttachment;
using DataAccess.BOL.InterventionAttachment;
using DataAccess.BOL.InterventionNote;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic
{
    /*[MB!] - Il y aurait eu moyen de faire qu'un AttachmentBLL qui gère les deux types d'attachement. Vous auriez fait une interface pour le transport et une propriété qui indique
        le type de l'attachment que votre classe concrète aurait de hardcoder ("Intervention", "Client"). C'est au niveau du DAL que vous auriez fait un switch sur cette propriété pour
        savoir dans quelle table faire l'opération. Les champs sont identiques ou presque entre les deux tables.
        Dans le cas de IdIntervention/IdClient, j'aurais mis une propriété IdElement et la classe concrète en dessous de l'interface pointerait sur IdIntervention ou IdClient dépendant.
    */
    public class InterventionAttachmentBLL : AbstractBLL<IInterventionAttachmentDAL>, IInterventionAttachmentBLL
    {
        public InterventionAttachmentBLL() { }
        public InterventionAttachmentBLL(ProviderDALTypes dalType) : base(dalType) { }
        public InterventionAttachmentBLL(IDAL externalDAL) : base(externalDAL) { }


        //[MB] - GetInterventionAttachment(int id)
        public GetItemResponse<InterventionAttachmentBOL> GetInterventionAttachmentById(int id)
        {
            var attachment = base.dal.GetInterventionAttachmentById(id);
            return new GetItemResponse<InterventionAttachmentBOL>(attachment);
        }

        //[MB] - GetInterventionAttachments(int interventionId)
        public GetListResponse<InterventionAttachmentBOL> GetInterventionAttachmentsByInterventionId(int interventionId)
        {
            var attachments = base.dal.GetInterventionAttachmentsByInterventionId(interventionId);
            return new GetListResponse<InterventionAttachmentBOL>(attachments);
        }


        public void CreateInterventionAttachment(InterventionAttachmentBOL attachment)
        {
            //[MB] - Validations? 
            base.dal.CreateInterventionAttachment(attachment);
        }

        public void DeleteInterventionAttachment(int id)
        {
            base.dal.DeleteInterventionAttachment(id);

        }

        //[MB] - Si vous voulez être propre pour cette méthode, il faut que votre DAL fasse une requête dans la BD avec un count et ensuite vous descendez ce count.
        // Il y a potentiellement un gain de performance important à faire plutôt que de matérialiser tous les objets en mémoire pour ensuite faire juste le décompte.
        //Sous sa forme actuelle, la méthode est inutile puisque le code qui fait l'appel aurait pu aussi faire :
        //      bll.GetInterventionAttachmentsByInterventionId(interventionId).ElementList.Count();
        public int GetInterventionAttachmentCount(int interventionId)
        {
            return GetInterventionAttachmentsByInterventionId(interventionId).ElementList.Count();
        }

        //[MB] - Je vous suggère de faire l'implémentation d'un delete en batch par le DAL. C'est beaucoup moins performant de faire des deletes un à un comme ça puisque vous allez 
        //ouvrir/fermer une connexion SQL à chaque fois. 
        //Aussi  renommer pour DeleteInterventionAttachments(int interventionId)
        public void DeleteAllInterventionAttachmentsByInterventionId(int interventionId)
        {
            List<InterventionAttachmentBOL> interventionAttachments = base.dal.GetInterventionAttachmentsByInterventionId(interventionId);
            foreach (var note in interventionAttachments)
            {
                base.dal.DeleteInterventionAttachment(note.Id);
            }
        }
    }
}
