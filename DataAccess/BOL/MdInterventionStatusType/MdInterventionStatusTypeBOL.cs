using DataAccess.Core.Definitions;
using DataModels.BOL.IMdInterventionStatusType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BOL.MdInterventionStatusType
{
    public class MdInterventionStatusTypeBOL : AbstractBOL<Models.MdInterventionStatusTypes>, IMdInterventionStatusTypeBOL
    {
        public MdInterventionStatusTypeBOL() { }
        public MdInterventionStatusTypeBOL(Models.MdInterventionStatusTypes record) : base(record) { }

        public int Id { get { return base.Record.Id; } }

        public string Name { get { return base.Record.Name; } set { base.Record.Name = value; } }
    }
}
