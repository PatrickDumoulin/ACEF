using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.BOL.InterventionsInterventionSolutions
{
    public interface IInterventionsInterventionSolutionsBOL : IBOL
    {
        public int Id { get; }

        public int IdIntervention { get; set; }

        public int IdInterventionSolution { get; set; }
    }
}
