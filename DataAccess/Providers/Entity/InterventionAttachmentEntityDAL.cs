using DataAccess.BOL.ClientAttachment;
using DataAccess.BOL.InterventionAttachment;
using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Entity
{
    public class InterventionAttachmentEntityDAL: AcefEntityDAL, IInterventionAttachmentDAL
    {
        public InterventionAttachmentEntityDAL() { }
        public InterventionAttachmentEntityDAL(AcefEntityDAL externalDal) : base(externalDal) { }

        public InterventionAttachmentBOL GetInterventionAttachmentById(int id)
        {
            var record = Db.InterventionsAttachments.FirstOrDefault(x => x.Id == id);
            return record != null ? new InterventionAttachmentBOL(record) : null;
        }

        public List<InterventionAttachmentBOL> GetInterventionAttachmentsByInterventionId(int interventionId)
        {
            var records = Db.InterventionsAttachments
                .Where(x => x.IdIntervention == interventionId)
                .ToList();
            return records.Select(record => new InterventionAttachmentBOL(record)).ToList();
        }

        
        public void CreateInterventionAttachment(InterventionAttachmentBOL interventionAttachment)
        {
            var entity = new InterventionsAttachments
            {
                IdIntervention = interventionAttachment.IdIntervention,
                IdEmployee = interventionAttachment.IdEmployee,
                FileName = interventionAttachment.FileName,
                FileContent = interventionAttachment.FileContent,
                CreatedDate = interventionAttachment.CreatedDate
            };
            Db.InterventionsAttachments.Add(entity);
            Db.SaveChanges();
        }

        public void DeleteInterventionAttachment(int id)
        {
            var attachment = Db.InterventionsAttachments.FirstOrDefault(x => x.Id == id);
            if (attachment != null)
            {
                Db.InterventionsAttachments.Remove(attachment);
                Db.SaveChanges();
            }
        }

        
    }
}
