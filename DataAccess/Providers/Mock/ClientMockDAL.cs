using DataAccess.BOL.Client;
using DataAccess.Core.Definitions;
using DataAccess.Providers.Interfaces;
using DataModels.BOL.Client;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Providers.Mock
{
    public class ClientMockDAL : AcefMockDAL, IClientDAL
    {

        private List<IClientBOL> clients = new List<IClientBOL>();

        public ClientMockDAL()
        {
            // Initialize with some mock data
            clients.Add(new ClientMockBOL { Id = 1, FirstName = "John", LastName = "Doe" });
            clients.Add(new ClientMockBOL { Id = 2, FirstName = "Jane", LastName = "Doe" });
        }

        public IClientBOL GetClient(int id)
        {
            return clients.FirstOrDefault(c => c.Id == id);
        }

        public List<IClientBOL> GetClients()
        {
            return clients;
        }

        public void CreateClient(IClientBOL client)
        {
            // Assume IDs are auto-incremented for simplicity
            int newId = clients.Any() ? clients.Max(c => c.Id) + 1 : 1;
            (client as ClientMockBOL).Id = newId; // Ensure the Id is set
            clients.Add(client);
        }

        public void UpdateClient(IClientBOL client)
        {
            var existingClient = clients.FirstOrDefault(c => c.Id == client.Id);
            if (existingClient != null)
            {
                var mockClient = existingClient as ClientMockBOL;
                mockClient.FirstName = client.FirstName;
                mockClient.LastName = client.LastName;
                mockClient.IsMember = client.IsMember;
                mockClient.Birthdate = client.Birthdate;
                mockClient.PhoneNumber = client.PhoneNumber;
                mockClient.Email = client.Email;
                mockClient.IdGenderDenomination = client.IdGenderDenomination;
                mockClient.Address = client.Address;
                mockClient.ZipCode = client.ZipCode;
                mockClient.IdMaritalStatus = client.IdMaritalStatus;
                mockClient.IdFamilySituation = client.IdFamilySituation;
                mockClient.AdultsAtHome = client.AdultsAtHome;
                mockClient.ChildsAtHome = client.ChildsAtHome;
                mockClient.IdHabitationType = client.IdHabitationType;
                mockClient.IdBank = client.IdBank;
                mockClient.IdEmploymentSituation = client.IdEmploymentSituation;
                mockClient.IdScholarshipType = client.IdScholarshipType;
                mockClient.Income = client.Income;
                mockClient.CreatedDate = client.CreatedDate;
                mockClient.LastModifiedDate = client.LastModifiedDate;
                // Update other fields as necessary
            }
        }

        public void DeleteClient(int id)
        {
            clients.RemoveAll(c => c.Id == id);
        }
    }
}
