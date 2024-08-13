using DataAccess.Core.Definitions;
using DataModels.BOL.ClientIncomeType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BOL.ClientIncomeType
{
    public class ClientIncomeTypeBOL : AbstractBOL<Models.ClientsIncomeTypes>, IClientIncomeTypeBOL
    {
        public ClientIncomeTypeBOL() { }
        public ClientIncomeTypeBOL(Models.ClientsIncomeTypes record): base(record) { }

        public int Id { get { return base.Record.Id; } }

        public int? IdClient { get { return base.Record.IdClient; } set { base.Record.IdClient = value; } }
    
        public int? IdIncomeType { get { return base.Record.IdIncomeType; } set { base.Record.IdIncomeType = value; } }
    }
}
