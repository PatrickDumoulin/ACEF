using DataAccess.BOL.ClientAttachment;
using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using DataModels.BOL.ClientAttachment;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Providers.Entity
{
    public class ClientAttachmentEntityDAL : AcefEntityDAL, IClientAttachmentDAL
    {
        public ClientAttachmentEntityDAL() { }

        public ClientAttachmentEntityDAL(AcefEntityDAL externalDal) : base(externalDal) { }

        public ClientAttachmentBOL GetClientAttachmentById(int id)
        {
            var record = Db.ClientsAttachments.FirstOrDefault(x => x.Id == id);
            return record != null ? new ClientAttachmentBOL(record) : null;
        }

        public List<ClientAttachmentBOL> GetClientAttachmentsByClientId(int clientId)
        {
            var records = Db.ClientsAttachments
                .Where(x => x.IdClient == clientId)
                .ToList();
            return records.Select(record => new ClientAttachmentBOL(record)).ToList();
        }

        public void CreateClientAttachment(ClientAttachmentBOL attachment)
        {
            //[MB] - Pas besoin de faire ça, vous pouvez directement faire
            //Db.ClientsAttachments.Add(attachment.Record);
            var entity = new ClientsAttachments
            {
                IdClient = attachment.IdClient,
                IdEmployee = attachment.IdEmployee,
                FileName = attachment.FileName,
                FileContent = attachment.FileContent,
                CreatedDate = attachment.CreatedDate
            };
            Db.ClientsAttachments.Add(entity);
            Db.SaveChanges();
        }

        public void DeleteClientAttachment(int id)
        {
            var attachment = Db.ClientsAttachments.FirstOrDefault(x => x.Id == id);
            if (attachment != null)
            {
                Db.ClientsAttachments.Remove(attachment);
                Db.SaveChanges();
            }
        }
    }
}
