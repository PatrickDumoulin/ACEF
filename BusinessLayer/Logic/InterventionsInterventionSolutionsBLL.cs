using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Core.Definitions;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.BOL.InterventionsInterventionSolutions;
using DataAccess.Providers.Interfaces;
using DataModels.BOL.Intervention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic
{
    public class InterventionsInterventionSolutionsBLL : AbstractBLL<IInterventionsInterventionSolutionsDAL>, IInterventionsInterventionSolutionsBLL
    {
        public InterventionsInterventionSolutionsBLL() { }
        public InterventionsInterventionSolutionsBLL(ProviderDALTypes dalType) : base(dalType) { }
        public InterventionsInterventionSolutionsBLL(IDAL externalDAL): base(externalDAL) { }

        public GetItemResponse<InterventionsInterventionSolutionsBOL> GetInterventionsInterventionSolution(int id)
        {
            var interventionsInterventionSolutions = base.dal.GetInterventionsInterventionSolution(id);
            return new GetItemResponse<InterventionsInterventionSolutionsBOL>(interventionsInterventionSolutions);
        }

        

        public GetListResponse<InterventionsInterventionSolutionsBOL> GetInterventionsInterventionSolutions()
        {
            var interventionsInterventionSolutions = base.dal.GetInterventionsInterventionSolutions();
            return new GetListResponse<InterventionsInterventionSolutionsBOL>(interventionsInterventionSolutions);
        }

        public void CreateInterventionsInterventionSolutions(InterventionsInterventionSolutionsBOL interventionsInterventionSolutionsBOL)
        {
            base.dal.CreateInterventionsInterventionSolutions(interventionsInterventionSolutionsBOL);
        }

        public void UpdateInterventionsInterventionSolutions(InterventionsInterventionSolutionsBOL interventionsInterventionSolutionsBOL)
        {
            base.dal.UpdateInterventionsInterventionSolutions(interventionsInterventionSolutionsBOL);
        }

        public void DeleteInterventionsInterventionSolutions(int id)
        {
            base.dal.DeleteInterventionsInterventionSolutions(id);
        }
    }
}
