using BusinessLayer.Communication.Responses.Common;
using CoreLib.Definitions;
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
        GetItemResponse<IClientBOL> GetClient(int id);
        GetListResponse<IClientBOL> GetClients();
        void CreateClient(IClientBOL clientBOL);
        void UpdateClient(IClientBOL clientBOL);

        string GetClientName(int clientId);



    }
}
