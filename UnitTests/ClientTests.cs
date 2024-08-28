using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Logic;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Injection;
using DataAccess.BOL.Client;
using DataAccess.BOL.CowMilk;
using DataAccess.Models;
using DataAccess.Providers.Mock;
using DataModels.BOL;
using DataModels.BOL.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Emit;
using System.Web.Helpers;

namespace UnitTests
{
    public class ClientTests
    {
        public ClientTests() { }

        public bool RunTests()
        {
            bool allSuccess = true;

            allSuccess &= getClientsList();
            allSuccess &= getClientById();
            allSuccess &= addNewClient();
            allSuccess &= updateExistingClient();
            allSuccess &= deleteClient();

            return allSuccess;
        }

        #region Get
        private bool getClientsList()
        {
            IClientBLL clientBLL = Injector.ImplementBll<IClientBLL>(CoreLib.Definitions.ProviderDALTypes.MOCK);
            List<IClientBOL> clients = clientBLL.GetClients().ElementList;

            bool allSuccess = clients.Any();
            foreach (IClientBOL client in clients)
            {
                allSuccess &= (client != null && !string.IsNullOrEmpty(client.FirstName));
            }

            return allSuccess;
        }

        private bool getClientById()
        {
            int clientId = 22; // Assume client with ID 1 exists
            IClientBLL clientBLL = Injector.ImplementBll<IClientBLL>(CoreLib.Definitions.ProviderDALTypes.MOCK);
            IClientBOL client = clientBLL.GetClient(clientId).Element;

            return client != null && client.Id == clientId;
        }
        #endregion

        #region Add
        public bool addNewClient()
        {
            // Utilise le MockDAL pour simuler les opérations de la base de données
            var clientDAL = new ClientMockDAL();

            // Injecte le MockDAL dans votre BLL
            var clientBLL = new ClientBLL(clientDAL);

            // Crée un nouveau client fictif
            IClientBOL newClient = new ClientMockBOL
            {
                FirstName = "New",
                LastName = "Client",
                Birthdate = DateTime.Now,
                PhoneNumber = "418-223-4444",
                Email = "yopatman@hotmail.com",
                IdGenderDenomination = 1,
                Address = "9160 av des ancetres",
                ZipCode = "G2B 1M4",
                IdMaritalStatus = 11,
                IdFamilySituation = 12,
                AdultsAtHome = 2,
                ChildsAtHome = 3,
                IdHabitationType = 18,
                IdBank = 52,
                IdEmploymentSituation = 44,
                IdScholarshipType = 25,
                Income = BitConverter.GetBytes(44444),
                CreatedDate = DateTime.Now,
                LastModifiedDate = DateTime.Now
            };

            // Ajoute le client au MockDAL
            clientBLL.CreateClient(newClient);

            // Récupère le client ajouté à partir du MockDAL
            IClientBOL dbClient = clientBLL.GetClient(newClient.Id).Element;

            // Vérifie que le client a bien été ajouté
            return dbClient != null && dbClient.FirstName == newClient.FirstName;
        }

        #endregion

        #region Update
        private bool updateExistingClient()
        {
            int clientId = 22; // Suppose que le client avec l'ID 1 existe
            IClientBLL clientBLL = Injector.ImplementBll<IClientBLL>(CoreLib.Definitions.ProviderDALTypes.MOCK);

            // Récupérer le client à mettre à jour
            IClientBOL clientToUpdate = clientBLL.GetClient(clientId).Element;

            if (clientToUpdate == null)
            {
                return false; // Si le client n'existe pas, le test échoue
            }

            // Modifier le nom de famille du client
            clientToUpdate.LastName = "UpdatedLastName";

            // Mettre à jour le client
            clientBLL.UpdateClient(clientToUpdate);

            // Récupérer le client après la mise à jour
            IClientBOL updatedClient = clientBLL.GetClient(clientId).Element;

            // Vérifier si la mise à jour a réussi
            return updatedClient != null && updatedClient.LastName == "UpdatedLastName";
        }
        #endregion

        #region Delete
        private bool deleteClient()
        {
            int clientId = 22; // Suppose que le client avec l'ID 1 existe
            IClientBLL clientBLL = Injector.ImplementBll<IClientBLL>(CoreLib.Definitions.ProviderDALTypes.MOCK);

            // Récupérer le client à supprimer
            IClientBOL clientToDelete = clientBLL.GetClient(clientId).Element;

            if (clientToDelete == null)
            {
                return false; // Si le client n'existe pas, le test échoue
            }

            // Marquer le client pour suppression
            clientToDelete.State = CoreLib.Definitions.ObjectState.DELETED;

            // Mettre à jour le client (suppression logique)
            clientBLL.UpdateClient(clientToDelete);

            // Récupérer le client après la suppression logique
            IClientBOL dbClient = clientBLL.GetClient(clientId).Element;

            // Vérifier si le client a été correctement supprimé
            return dbClient == null || dbClient.State == CoreLib.Definitions.ObjectState.DELETED;
        }
        #endregion
    }
}
