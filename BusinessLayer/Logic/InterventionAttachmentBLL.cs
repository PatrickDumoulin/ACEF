using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Core.Definitions;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.BOL.ClientAttachment;
using DataAccess.BOL.InterventionAttachment;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic
{
    public class InterventionAttachmentBLL : AbstractBLL<IInterventionAttachmentDAL>, IInterventionAttachmentBLL
    {
        public InterventionAttachmentBLL() { }
        public InterventionAttachmentBLL(ProviderDALTypes dalType) : base(dalType) { }
        public InterventionAttachmentBLL(IDAL externalDAL) : base(externalDAL) { }


        public GetItemResponse<InterventionAttachmentBOL> GetInterventionAttachmentById(int id)
        {
            var attachment = base.dal.GetInterventionAttachmentById(id);
            return new GetItemResponse<InterventionAttachmentBOL>(attachment);
        }

        public GetListResponse<InterventionAttachmentBOL> GetInterventionAttachmentsByInterventionId(int interventionId)
        {
            var attachments = base.dal.GetInterventionAttachmentsByInterventionId(interventionId);
            return new GetListResponse<InterventionAttachmentBOL>(attachments);
        }
        public void CreateInterventionAttachment(InterventionAttachmentBOL attachment)
        {
            base.dal.CreateInterventionAttachment(attachment);
        }

        public void DeleteInterventionAttachment(int id)
        {
            base.dal.DeleteInterventionAttachment(id);

        }

        public int GetInterventionAttachmentCount(int interventionId)
        {
            return GetInterventionAttachmentsByInterventionId(interventionId).ElementList.Count();
        }
    }
}
