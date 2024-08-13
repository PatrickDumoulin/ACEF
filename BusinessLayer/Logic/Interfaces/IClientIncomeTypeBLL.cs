using BusinessLayer.Communication.Responses.Common;
using CoreLib.Definitions;
using DataAccess.BOL.ClientAttachment;
using DataAccess.BOL.ClientIncomeType;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic.Interfaces
{
    public interface IClientIncomeTypeBLL : IBLL
    {
        GetItemResponse<ClientIncomeTypeBOL> GetClientIncomeTypeById(int id);
        GetListResponse<ClientIncomeTypeBOL> GetClientIncomeTypesByClientId(int clientId);
        void CreateClientIncomeType(ClientIncomeTypeBOL incomeType);
        void DeleteClientIncomeType(int id);
    }
}
