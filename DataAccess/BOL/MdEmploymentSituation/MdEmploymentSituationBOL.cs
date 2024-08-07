using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataModels.BOL.MdEmploymentSituation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BOL.MdEmploymentSituation
{
    public class MdEmploymentSituationBOL : AbstractBOL<Models.MdEmploymentSituation>, IMdEmploymentSituationBOL
    {
        public MdEmploymentSituationBOL() { }
        public MdEmploymentSituationBOL(Models.MdEmploymentSituation record) : base(record) { }


        public int Id { get { return base.Record.Id; } }

        public string Name { get { return base.Record.Name; } set { base.Record.Name = value; } }

        public bool? Active { get { return base.Record.Active; } set { base.Record.Active = value; } }
    }
}
