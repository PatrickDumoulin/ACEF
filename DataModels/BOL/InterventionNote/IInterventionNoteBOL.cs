using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.BOL.InterventionNote
{
    public interface IInterventionNoteBOL : IBOL
    {
        public int Id { get; }

        public int? IdIntervention { get; set; }

        public int? IdEmployee { get; set; }

        public string Comment { get; set; }

        public DateTime? CreatedDate { get; set; }
    }
}
