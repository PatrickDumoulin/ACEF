using BusinessLayer.Communication.Responses.Common;
using CoreLib.Definitions;
using DataAccess.BOL.ClientAttachment;
using System.Collections.Generic;

namespace BusinessLayer.Logic.Interfaces
{
    public interface IClientAttachmentBLL : IBLL
    {
        GetItemResponse<ClientAttachmentBOL> GetAttachmentById(int id);
        GetListResponse<ClientAttachmentBOL> GetAttachmentsByClientId(int clientId);
        void CreateAttachment(ClientAttachmentBOL attachment);
        void DeleteAttachment(int id);
    }
}
