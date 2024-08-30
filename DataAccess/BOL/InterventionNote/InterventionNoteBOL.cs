using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataModels.BOL.InterventionNote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BOL.InterventionNote
{
    public class InterventionNoteBOL : AbstractBOL<InterventionsNotes>, IInterventionNoteBOL
    {
        public InterventionNoteBOL() { }

        public InterventionNoteBOL(InterventionsNotes record) : base(record) { }

        public int Id { get { return base.Record.Id; } set { base.Record.Id = value; } }

        public int? IdIntervention { get { return base.Record.IdIntervention; } set { base.Record.IdIntervention = value; } }

        public int? IdEmployee { get { return base.Record.IdEmployee; } set { base.Record.IdEmployee = value; } }

        public string Comment { get { return base.Record.Comment; } set { base.Record.Comment = value; } }

        public DateTime? CreatedDate { get { return base.Record.CreatedDate; } set { base.Record.CreatedDate = value; } }

    }
}
