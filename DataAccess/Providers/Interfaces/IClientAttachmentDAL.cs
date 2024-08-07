using CoreLib.Definitions;
using DataAccess.BOL.ClientAttachment;
using System.Collections.Generic;

namespace DataAccess.Providers.Interfaces
{
    public interface IClientAttachmentDAL : IDAL
    {
        ClientAttachmentBOL GetClientAttachmentById(int id);
        List<ClientAttachmentBOL> GetClientAttachmentsByClientId(int clientId);
        void CreateClientAttachment(ClientAttachmentBOL attachment);
        void DeleteClientAttachment(int id);
    }
}
