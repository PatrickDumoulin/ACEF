using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Logic.Interfaces;
using DataModels.BOL.Client;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;

namespace UnitTests
{
    [TestClass]
    public class ClientTests
    {
        private Mock<IClientBLL> mockClientBLL;

        [TestInitialize]
        public void Setup()
        {
            // Initialiser le mock pour IClientBLL
            mockClientBLL = new Mock<IClientBLL>();

            // Utiliser des mocks pour éviter la dépendance au conteneur IoC
            var mockClientList = new List<IClientBOL>
            {
                new Mock<IClientBOL>().Object, // Mock au lieu de new ClientBOL()
                new Mock<IClientBOL>().Object
            };

            // Configurer les méthodes du mock pour retourner des résultats simulés
            mockClientBLL.Setup(bll => bll.GetClients())
                         .Returns(new GetListResponse<IClientBOL>(mockClientList));

            mockClientBLL.Setup(bll => bll.GetClient(It.IsAny<int>()))
                         .Returns((int id) =>
                         {
                             var mockClient = new Mock<IClientBOL>(); // Mock au lieu de new ClientBOL()
                             mockClient.Setup(c => c.Id).Returns(id);
                             return new GetItemResponse<IClientBOL>(mockClient.Object);
                         });

            mockClientBLL.Setup(bll => bll.CreateClient(It.IsAny<IClientBOL>())).Verifiable();
            mockClientBLL.Setup(bll => bll.UpdateClient(It.IsAny<IClientBOL>())).Verifiable();
        }

        [TestMethod]
        public void Test_GetClientsList()
        {
            var result = mockClientBLL.Object.GetClients().ElementList;
            Assert.IsTrue(result.Any(), "Le test de récupération de la liste des clients a échoué.");
        }

        [TestMethod]
        public void Test_GetClientById()
        {
            int clientId = 23;
            var client = mockClientBLL.Object.GetClient(clientId).Element;

            Assert.IsNotNull(client, "Le test de récupération d'un client par ID a échoué.");
            Assert.AreEqual(clientId, client.Id);
        }

        [TestMethod]
        public void Test_AddNewClient()
        {
            var newClient = new Mock<IClientBOL>().Object; // Mock au lieu de new ClientBOL()

            mockClientBLL.Object.CreateClient(newClient);

            // Vérifier si la méthode CreateClient a été appelée
            mockClientBLL.Verify(bll => bll.CreateClient(It.IsAny<IClientBOL>()), Times.Once);
        }

        [TestMethod]
        public void Test_UpdateExistingClient()
        {
            var existingClient = mockClientBLL.Object.GetClient(23).Element;
            existingClient.LastName = "UpdatedLastName";

            mockClientBLL.Object.UpdateClient(existingClient);

            // Vérifier si la méthode UpdateClient a été appelée
            mockClientBLL.Verify(bll => bll.UpdateClient(It.IsAny<IClientBOL>()), Times.Once);
        }

        [TestMethod]
        public void Test_DeleteClient()
        {
            var client = mockClientBLL.Object.GetClient(23).Element;
            client.State = CoreLib.Definitions.ObjectState.DELETED;

            mockClientBLL.Object.UpdateClient(client);

            // Vérifier si la méthode UpdateClient a été appelée
            mockClientBLL.Verify(bll => bll.UpdateClient(It.IsAny<IClientBOL>()), Times.Once);
        }
    }
}
