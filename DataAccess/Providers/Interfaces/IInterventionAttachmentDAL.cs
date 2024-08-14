using CoreLib.Definitions;
using DataAccess.BOL.ClientAttachment;
using DataAccess.BOL.InterventionAttachment;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Interfaces
{
    public interface IInterventionAttachmentDAL : IDAL
    {
        InterventionAttachmentBOL GetInterventionAttachmentById(int id);
        List<InterventionAttachmentBOL> GetInterventionAttachmentsByInterventionId(int interventionId);
        void CreateInterventionAttachment(InterventionAttachmentBOL interventionAttachment);
        void DeleteInterventionAttachment(int id);
    }
}
