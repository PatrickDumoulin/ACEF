using DataAccess.Core.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DataModels.BOL.MdBank;

namespace DataAccess.BOL.MdBank
{
    public class MdBankBOL : AbstractBOL<Models.MdBank>, IMdBankBOL
    {
        public MdBankBOL() { }
        public MdBankBOL(Models.MdBank record) : base(record) { }

        public int Id { get { return base.Record.Id; } }

        //[MB!] - Pourquoi les validations de Null? Ça me semble superflu. Ça semble être dans plusieurs MD, mais ils ont tous un name dans la BD
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
        public bool IsDesjardins { get { return base.Record.IsDesjardins; } set { base.Record.IsDesjardins = value; } }
    }
}
