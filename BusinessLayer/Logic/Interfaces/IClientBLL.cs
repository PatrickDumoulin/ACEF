using BusinessLayer.Communication.Responses.Common;
using CoreLib.Definitions;
using DataAccess.BOL.Client;
using DataModels.BOL.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic.Interfaces
{
	public interface IClientBLL : IBLL
	{
        GetItemResponse<ClientBOL> GetClient(int id);
        GetListResponse<ClientBOL> GetClients();
        void CreateClient(ClientBOL clientBOL);
        void UpdateClient(IClientBOL clientBOL);
        string GetClientName(int clientId);
        int GetClientAge(int clientId);



    }
}
