using CoreLib.Definitions;
using DataAccess.BOL.Client;
using DataModels.BOL.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Interfaces
{
	public interface IClientDAL : IDAL
	{
		IClientBOL GetClient(int id);

        List<IClientBOL> GetClients();
		void CreateClient(IClientBOL client);
		void UpdateClient(IClientBOL client);
    }
}
