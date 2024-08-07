using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataModels.BOL.Intervention;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BOL.Intervention
{
    public class InterventionBOL : AbstractBOL<Interventions>, IInterventionBOL
    {
        public InterventionBOL() { }
        public InterventionBOL(Interventions record) : base(record) { }

        public int Id { get { return base.Record.Id; } }

        public bool IsVirtual { get { return base.Record.IsVirtual ?? false; } set { base.Record.IsVirtual = value; } }

        public DateTime? DateIntervention { get { return base.Record.DateIntervention; } set { base.Record.DateIntervention = value; } }

        public int? IdEmployee { get { return base.Record.IdEmployee; } set { base.Record.IdEmployee = value; } }

        public int? IdClient { get { return base.Record.IdClient; } set { base.Record.IdClient = value; } }

        public int? IdReferenceType { get { return base.Record.IdReferenceType; } set { base.Record.IdReferenceType = value; } }

        public int? IdStatusType { get { return base.Record.IdStatusType; } set { base.Record.IdStatusType = value; } }

        public int? IdInterventionType { get { return base.Record.IdInterventionType; } set { base.Record.IdInterventionType = value; } }

        public byte[] DebtAmount { get { return base.Record.DebtAmount; } set { base.Record.DebtAmount = value; } }

        public int? IdLoanReason { get { return base.Record.IdLoanReason; } set { base.Record.IdLoanReason = value; } }

        public bool? IsLoanPaid { get { return base.Record.IsLoanPaid; } set { base.Record.IsLoanPaid = value; } }
    }
}
