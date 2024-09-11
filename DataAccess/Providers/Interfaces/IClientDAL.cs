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
		public ClientBOL GetClient(int id);

		public List<ClientBOL> GetClients();

		public void CreateClient(ClientBOL clientBOL);

        void UpdateClient(IClientBOL client);
    }
}
