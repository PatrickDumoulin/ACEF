using DataAccess.Core.Definitions;
using DataModels.BOL.MdFamilySituation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BOL.MdFamilySituation
{
    public class MdFamilySituationBOL : AbstractBOL<Models.MdFamilySituation>, IMdFamilySituationBOL
    {
        public MdFamilySituationBOL() { }
        public MdFamilySituationBOL(Models.MdFamilySituation record) : base(record) { }

        public int Id { get { return base.Record.Id; } }

        public string Name { get { return base.Record.Name; } set { base.Record.Name = value; } }

        public bool? Active { get { return base.Record.Active; } set { base.Record.Active = value; } }
    }
}
