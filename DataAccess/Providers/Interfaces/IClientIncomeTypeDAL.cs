using CoreLib.Definitions;
using DataAccess.BOL.ClientAttachment;
using DataAccess.BOL.ClientIncomeType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Interfaces
{
    public interface IClientIncomeTypeDAL : IDAL
    {
        ClientIncomeTypeBOL GetClientIncomeTypeById(int id);
        List<ClientIncomeTypeBOL> GetClientIncomeTypesByClientId(int clientId);
        void CreateClientIncomeType(ClientIncomeTypeBOL incomeType);
        void DeleteClientIncomeType(int id);
    }
}
