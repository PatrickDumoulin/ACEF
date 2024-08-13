using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.BOL.ClientIncomeType
{
    public interface IClientIncomeTypeBOL : IBOL
    {
        public int Id { get; }

        public int? IdClient { get; set; }

        public int? IdIncomeType { get; set; }
    }
}
