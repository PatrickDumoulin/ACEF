using CoreLib.Definitions;
using DataAccess.BOL.Intervention;
using DataAccess.BOL.InterventionsInterventionSolutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Interfaces
{
    public interface IInterventionsInterventionSolutionsDAL : IDAL
    {
        InterventionsInterventionSolutionsBOL GetInterventionsInterventionSolution(int id);
        List<InterventionsInterventionSolutionsBOL> GetInterventionsInterventionSolutions();
        void CreateInterventionsInterventionSolutions(InterventionsInterventionSolutionsBOL interventionsInterventionSolutionsBOL);
        void UpdateInterventionsInterventionSolutions(InterventionsInterventionSolutionsBOL interventionsInterventionSolutionsBOL);
        void DeleteInterventionsInterventionSolutions(int id);
    }
}
