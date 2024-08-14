using DataAccess.Core.Definitions;
using DataModels.BOL.InterventionAttachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BOL.InterventionAttachment
{
    public class InterventionAttachmentBOL : AbstractBOL<Models.InterventionsAttachments>, IInterventionAttachmentBOL
    {
        public InterventionAttachmentBOL() { }
        public InterventionAttachmentBOL(Models.InterventionsAttachments record) : base(record) { } 
        public int Id { get { return base.Record.Id; } }

        public int? IdIntervention { get { return base.Record.IdIntervention; } set { base.Record.IdIntervention = value; } }

        public int? IdEmployee { get { return base.Record.IdEmployee; } set { base.Record.IdEmployee = value; } }

        public string FileName { get { return base.Record.FileName; } set { base.Record.FileName = value; } }

        public byte[] FileContent { get { return base.Record.FileContent; } set { base.Record.FileContent = value; } }

        public DateTime? CreatedDate { get { return base.Record.CreatedDate; } set { base.Record.CreatedDate = value; } }
    }
}
