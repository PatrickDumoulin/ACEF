using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using DataModels.BOL.SeminarEmployee;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BOL.SeminarEmployee
{
    public class SeminarEmployeeBOL : AbstractBOL<SeminarsEmployees>, ISeminarEmployeeBOL
    {
        public SeminarEmployeeBOL() { }
        public SeminarEmployeeBOL(SeminarsEmployees record): base(record) { }

        public int Id { get { return base.Record.Id; } }

        public int? IdSeminar { get { return base.Record.IdSeminar; } set { base.Record.IdSeminar = value; } }

        public int? IdEmployee { get { return base.Record.IdEmployee; } set { base.Record.IdEmployee = value; } }
    }
}
