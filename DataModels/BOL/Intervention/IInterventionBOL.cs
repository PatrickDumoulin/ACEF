using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.BOL.Intervention
{
    public interface IInterventionBOL: IBOL
    {
        public int Id { get; }

        public bool IsVirtual { get; set; }

        public DateTime? DateIntervention { get; set; }

        public int? IdEmployee { get; set; }

        public int? IdClient { get; set; }

        public int? IdReferenceType { get; set; }

        public int? IdStatusType { get; set; }

        public int? IdInterventionType { get; set; }

        public byte[] DebtAmount { get; set; }

        public int? IdLoanReason { get; set; }

        public bool? IsLoanPaid { get; set; }
    }
}
