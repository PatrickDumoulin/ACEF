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

        public GetListResponse<ClientAttachmentBOL> GetAttachmentsByClientId(int clientId)
        {
            var attachments = base.dal.GetClientAttachmentsByClientId(clientId);
            return new GetListResponse<ClientAttachmentBOL>(attachments);
        }

        public GetItemResponse<ClientAttachmentBOL> GetAttachmentById(int id)
        {
            var attachment = base.dal.GetClientAttachmentById(id);
            return new GetItemResponse<ClientAttachmentBOL>(attachment);
        }

        public void CreateAttachment(ClientAttachmentBOL attachment)
        {
            base.dal.CreateClientAttachment(attachment);
        }

        public void DeleteAttachment(int id)
        {
            base.dal.DeleteClientAttachment(id);
        }
    }
}
