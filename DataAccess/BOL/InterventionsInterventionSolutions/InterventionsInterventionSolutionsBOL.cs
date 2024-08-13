using DataAccess.Core.Definitions;
using DataModels.BOL.InterventionsInterventionSolutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BOL.InterventionsInterventionSolutions
{
    public class InterventionsInterventionSolutionsBOL : AbstractBOL<Models.InterventionsInterventionSolutions>, IInterventionsInterventionSolutionsBOL
    {
        public InterventionsInterventionSolutionsBOL() { }
        public InterventionsInterventionSolutionsBOL(Models.InterventionsInterventionSolutions record): base(record) { }

        public int Id { get { return base.Record.Id; } }

        public int IdIntervention
        {
            get { return base.Record.IdIntervention; }
            set { base.Record.IdIntervention = value; }
        }

        public int IdInterventionSolution
        {
            get { return base.Record.IdInterventionSolution; }
            set { base.Record.IdInterventionSolution = value; }
        }
    }

}

