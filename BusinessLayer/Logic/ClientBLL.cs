using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Core.Definitions;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.BOL.Client;
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

        public GetItemResponse<ClientBOL> GetClient(int id)
        {
            ClientBOL clientBOL = base.dal.GetClient(id);
            return new GetItemResponse<ClientBOL>(clientBOL);
        }

        public GetListResponse<ClientBOL> GetClients()
        {
            List<ClientBOL> clientsBOL = base.dal.GetClients();
            return new GetListResponse<ClientBOL>(clientsBOL);
        }


        public void CreateClient(ClientBOL clientBOL)
        {
            //[MB] - Aucunes validations?
            base.dal.CreateClient(clientBOL);
        }

        //[MB] - Pourquoi ici vous utilisez une interface? L'idée est bonne, mais on devrait le voir partout alors et non partiellement. 
        public void UpdateClient(IClientBOL clientBOL)
        {
            //[MB] - Aucunes validations?
            base.dal.UpdateClient(clientBOL);
        }

        /*[MB!] - Cette méthode ne sert pas à grand chose sinon maintenir du code supplémentaire pour rien. Puisque vous fetcher l'objet au complet, il n'y pas de gains de performance
                 et on préfère plutôt que l'appel soit fait directement sur GetClient(). 
                 À voir aussi qu'on récupère qu'un seul row, il n'y a pas besoin d'essayer de gagner dans la performance pour une opération simple. 
         */
        public string GetClientName(int clientId)
        {
            var clientResponse = GetClient(clientId);
            return clientResponse.Succeeded && clientResponse.Element != null
                ? $"{clientResponse.Element.FirstName} {clientResponse.Element.LastName}"
                : "Inconnu";
        }

        /*[MB] - Idem que plus haut. Ceci dit, on devrait voir cette méthode à même une instance de ClientBOL. C'est-à-dire qu'il devrait y avoir une méthode GetClientAge qui calcule l'âge
            en fonction des données de l'instance.
        */
        public int GetClientAge(int clientId)
        {
            var clientResponse = GetClient(clientId);
            if (clientResponse.Succeeded && clientResponse.Element != null && clientResponse.Element.Birthdate.HasValue)
            {
                var today = DateTime.Today;
                var birthdate = clientResponse.Element.Birthdate.Value;
                var age = today.Year - birthdate.Year;

                // Ajuster l'âge si l'anniversaire n'est pas encore passé cette année
                if (birthdate.Date > today.AddYears(-age)) age--;

                return age;
            }

            // Retourner -1 si l'âge ne peut être déterminé
            return -1;
        }


    }
}

