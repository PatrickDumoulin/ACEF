using DataAccess.Core.Definitions;
using DataModels.BOL.MdIncomeType;
using System;

namespace DataAccess.BOL.MdIncomeType
{
    public class MdIncomeTypeBOL : AbstractBOL<Models.MdIncomeType>, IMdIncomeTypeBOL
    {
        public MdIncomeTypeBOL() { }

        public MdIncomeTypeBOL(Models.MdIncomeType record) : base(record) { }

        public int Id { get { return base.Record.Id; } }

        public string Name
        {
            get { return base.Record?.Name ?? "Non spécifié"; }
            set
            {
                if (base.Record != null)
                {
                    base.Record.Name = value;
                }
            }
        }

        public bool? Active { get { return base.Record.Active; } set { base.Record.Active = value; } }
    }
}
