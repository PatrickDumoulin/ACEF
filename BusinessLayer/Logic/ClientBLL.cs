using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Core.Definitions;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.Providers.Interfaces;
using DataModels.BOL.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic
{
	public class ClientBLL : AbstractBLL<IClientDAL>, IClientBLL
	{
        public ClientBLL() { }
        public ClientBLL(ProviderDALTypes dalType) : base(dalType) { }
        public ClientBLL(IDAL externalDAL) : base(externalDAL) { }

        public GetItemResponse<IClientBOL> GetClient(int id)
        {
            IClientBOL clientBOL = base.dal.GetClient(id);
            return new GetItemResponse<IClientBOL>(clientBOL);
        }

        public GetListResponse<IClientBOL> GetClients()
        {
            List<IClientBOL> clientsBOL = base.dal.GetClients();
            return new GetListResponse<IClientBOL>(clientsBOL);
        }


        public void CreateClient(IClientBOL clientBOL)
        {
            base.dal.CreateClient(clientBOL);
        }

        public void UpdateClient(IClientBOL clientBOL)
        {
            base.dal.UpdateClient(clientBOL);
        }

        
    }
}

