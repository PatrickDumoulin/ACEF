using BusinessLayer.Communication.Responses.Common;
using CoreLib.Definitions;
using DataAccess.BOL.ClientAttachment;
using DataAccess.BOL.InterventionAttachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic.Interfaces
{
    public interface IInterventionAttachmentBLL : IBLL
    {
        GetItemResponse<InterventionAttachmentBOL> GetInterventionAttachmentById(int id);
        GetListResponse<InterventionAttachmentBOL> GetInterventionAttachmentsByInterventionId(int interventionId);
        void CreateInterventionAttachment(InterventionAttachmentBOL attachment);
        void DeleteInterventionAttachment(int id);

        int GetInterventionAttachmentCount(int interventionId);
    }
}
