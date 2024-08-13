using BusinessLayer.Communication.Responses.Common;
using CoreLib.Definitions;
using DataAccess.BOL.InterventionsInterventionSolutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic.Interfaces
{
    public interface IInterventionsInterventionSolutionsBLL : IBLL
    {
        GetItemResponse<InterventionsInterventionSolutionsBOL> GetInterventionsInterventionSolution(int id);

        GetListResponse<InterventionsInterventionSolutionsBOL> GetInterventionsInterventionSolutions();

        void CreateInterventionsInterventionSolutions(InterventionsInterventionSolutionsBOL interventionsInterventionSolutionsBOL);

        void UpdateInterventionsInterventionSolutions(InterventionsInterventionSolutionsBOL interventionsInterventionSolutionsBOL);

        void DeleteInterventionsInterventionSolutions(int id);
    }
}
