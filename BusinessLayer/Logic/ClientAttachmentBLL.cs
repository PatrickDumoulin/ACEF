using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Core.Definitions;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.BOL.ClientAttachment;
using DataAccess.Providers.Interfaces;
using System.Collections.Generic;

namespace BusinessLayer.Logic
{
    public class ClientAttachmentBLL : AbstractBLL<IClientAttachmentDAL>, IClientAttachmentBLL
    {
        public ClientAttachmentBLL() { }
        public ClientAttachmentBLL(ProviderDALTypes dalType) : base(dalType) { }
        public ClientAttachmentBLL(IDAL externalDAL) : base(externalDAL) { }

        /*[MB!] - Généralement on préfère faire utilisation de surcharge de méthodes plutôt que spécifier dans le nom de méthode c'est par quoi on recherche.
            Ainsi on a l'un pour un élément au singulier et l'autre pour une liste. 
                       GetAttachment(int id)
                       GetAttachments(int clientId) 

                        Si on veut en ajouter un nouveau, par exemple
                        GetAttachment(string FileName)  

           On utilise le By dans le nom lorsqu'il y a conflit et que le paramètre est déjà utilisé
                      GetAttachmentsByIdEmployee(int idEmployee)
         */
        public GetItemResponse<ClientAttachmentBOL> GetAttachmentById(int id)
        {
            var attachment = base.dal.GetClientAttachmentById(id);
            return new GetItemResponse<ClientAttachmentBOL>(attachment);
        }

        public GetListResponse<ClientAttachmentBOL> GetAttachmentsByClientId(int clientId)
        {
            var attachments = base.dal.GetClientAttachmentsByClientId(clientId);
            return new GetListResponse<ClientAttachmentBOL>(attachments);
        }

        

        public void CreateAttachment(ClientAttachmentBOL attachment)
        {
            /*[MB] - Aucunes validations? 
                FileName != String.IsNullEmpty, 
                FileContent != null et > 0 bytes,
                etc
            */

            base.dal.CreateClientAttachment(attachment);
        }

        public void DeleteAttachment(int id)
        {
            base.dal.DeleteClientAttachment(id);
        }
    }
}
