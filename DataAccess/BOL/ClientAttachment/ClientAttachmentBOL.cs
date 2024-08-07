using CoreLib.Definitions;
using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataModels.BOL.ClientAttachment;

namespace DataAccess.BOL.ClientAttachment
{
    public class ClientAttachmentBOL : AbstractBOL<ClientsAttachments>, IClientAttachmentBOL
    {
        public ClientAttachmentBOL() { }

        public ClientAttachmentBOL(ClientsAttachments record) : base(record) { }

        public int Id
        {
            get { return base.Record.Id; }
            set { base.Record.Id = value; }
        }
        public int? IdClient
        {
            get { return base.Record.IdClient; }
            set { base.Record.IdClient = value; }
        }
        public int? IdEmployee
        {
            get { return base.Record.IdEmployee; }
            set { base.Record.IdEmployee = value; }
        }
        public string FileName
        {
            get { return base.Record.FileName; }
            set { base.Record.FileName = value; }
        }
        public byte[] FileContent
        {
            get { return base.Record.FileContent; }
            set { base.Record.FileContent = value; }
        }
        public DateTime? CreatedDate
        {
            get { return base.Record.CreatedDate; }
            set { base.Record.CreatedDate = value; }
        }
    }
}
