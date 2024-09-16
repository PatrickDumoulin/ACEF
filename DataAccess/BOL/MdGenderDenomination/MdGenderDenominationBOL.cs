using DataAccess.Core.Definitions;
using DataModels.BOL.MdGenderDenomination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BOL.MdGenderDenomination
{
    public class MdGenderDenominationBOL : AbstractBOL<Models.MdGenderDenomination>, IMdGenderDenominationBOL
    {
        public MdGenderDenominationBOL() { }
        public MdGenderDenominationBOL(Models.MdGenderDenomination record) : base(record) { }

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
