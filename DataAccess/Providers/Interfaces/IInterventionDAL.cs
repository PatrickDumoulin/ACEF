using CoreLib.Definitions;
using DataAccess.BOL.Intervention;
using DataModels.BOL.Client;
using DataModels.BOL.Intervention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Interfaces
{
    public interface IInterventionDAL : IDAL
    {
        InterventionBOL GetIntervention(int id);
        List<InterventionBOL> GetInterventions();
        void CreateIntervention(InterventionBOL interventionBOL);
        void UpdateIntervention(InterventionBOL interventionBOL);
        void DeleteIntervention(int id);
    }
}
