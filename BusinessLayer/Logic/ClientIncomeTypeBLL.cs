using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Core.Definitions;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.BOL.ClientAttachment;
using DataAccess.BOL.ClientIncomeType;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic
{
    /*[MB] - Les méthodes devraient être dans ClientBLL*/
    public class ClientIncomeTypeBLL : AbstractBLL<IClientIncomeTypeDAL>, IClientIncomeTypeBLL
    {
        public ClientIncomeTypeBLL() { }
        public ClientIncomeTypeBLL(ProviderDALTypes dalType) : base(dalType) { }
        public ClientIncomeTypeBLL(IDAL externalDAL) : base (externalDAL) { }


        public GetItemResponse<ClientIncomeTypeBOL> GetClientIncomeTypeById(int id)
        {
            var incomeType = base.dal.GetClientIncomeTypeById(id);
            return new GetItemResponse<ClientIncomeTypeBOL>(incomeType);
        }

        public GetListResponse<ClientIncomeTypeBOL> GetClientIncomeTypesByClientId(int clientId)
        {
            var incomeTypes = base.dal.GetClientIncomeTypesByClientId(clientId);
            return new GetListResponse<ClientIncomeTypeBOL>(incomeTypes);
        }
        public void CreateClientIncomeType(ClientIncomeTypeBOL incomeType)
        {
            base.dal.CreateClientIncomeType(incomeType);
        }

        public void DeleteClientIncomeType(int id)
        {
            base.dal.DeleteClientIncomeType(id);
        }

    }
}
