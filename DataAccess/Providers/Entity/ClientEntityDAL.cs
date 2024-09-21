using CoreLib.Definitions;
using DataAccess.BOL.Client;

using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using DataModels.BOL.Client;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Entity
{
	public class ClientEntityDAL : AcefEntityDAL, IClientDAL
	{
		public ClientEntityDAL() { }
		public ClientEntityDAL(AcefEntityDAL externalDal): base(externalDal) { }



        public ClientBOL GetClient(int id)
        {
            var record = Db.Clients
                           .Include(i => i.ClientsIncomeTypes) // Inclure les IncomeTypes
                           .FirstOrDefault(x => x.Id == id);
            return MapperWrapper.NewBol<ClientBOL>(record);
        }


        public List<ClientBOL> GetClients()
        {
            var records = Db.Clients.Include(i => i.ClientsIncomeTypes).ToList();
            return records.Select(x => new ClientBOL(x)).ToList();
        }

        public void CreateClient(ClientBOL clientBOL)
        {
            if (clientBOL == null)
            {
                throw new ArgumentNullException(nameof(clientBOL), "ClientBOL cannot be null");
            }

            using (var transaction = Db.Database.BeginTransaction())
            {
                try
                {
                    //[MB!] - Pas besoin de faire ça, vous pouvez directement faire
                    //base.SaveEntity(clientBOL); 
                    //Revoir CowMilkEntityDAL

                    //[MB!] - De plus, vous avez déjà l'identifiant de votre objet, pas besoin de faire ça
                    //clientBOL.Id = newClient.Id
                    //Quand vous faites un new BOL(), si celui-ci est ISequenced, il va gérer tout seul dans l'abstractBOL d'obtenir le prochain ID de la séquence et l'assigner à votre record.
                    //Je pense que vous aurez à revoir tous vos DAL en conséquence

                    var newClient = new Clients
                    {
                        IsMember = clientBOL.IsMember,
                        LastName = clientBOL.LastName,
                        FirstName = clientBOL.FirstName,
                        Birthdate = clientBOL.Birthdate,
                        PhoneNumber = clientBOL.PhoneNumber,
                        Email = clientBOL.Email,
                        IdGenderDenomination = clientBOL.IdGenderDenomination,
                        Address = clientBOL.Address,
                        ZipCode = clientBOL.ZipCode,
                        IdMaritalStatus = clientBOL.IdMaritalStatus,
                        IdFamilySituation = clientBOL.IdFamilySituation,
                        AdultsAtHome = clientBOL.AdultsAtHome,
                        ChildsAtHome = clientBOL.ChildsAtHome,
                        IdHabitationType = clientBOL.IdHabitationType,
                        IdBank = clientBOL.IdBank,
                        IdEmploymentSituation = clientBOL.IdEmploymentSituation,
                        IdScholarshipType = clientBOL.IdScholarshipType,
                        Income = clientBOL.Income,
                        CreatedDate = DateTime.Now,
                        LastModifiedDate = DateTime.Now
                    };

                    Db.Clients.Add(newClient);
                    Db.SaveChanges();

                    // Mettre à jour l'ID de l'objet BOL après l'insertion
                    clientBOL.Id = newClient.Id;

                    // Ajouter les solutions associées
                    foreach (var incomeType in clientBOL.ClientIncomeTypes)
                    {
                        var clientIncomeType = new ClientsIncomeTypes
                        {
                            IdClient = newClient.Id,
                            IdIncomeType = incomeType.IdIncomeType
                        };

                        Db.ClientsIncomeTypes.Add(clientIncomeType);
                    }

                    Db.SaveChanges();
                    transaction.Commit();
                }
                catch (Exception)
                {
                    transaction.Rollback();
                    throw;
                }
            }
        }


        
        public void UpdateClient(IClientBOL clientBOL)
        {
            if (clientBOL == null)
            {
                throw new ArgumentNullException(nameof(clientBOL), "ClientBOL cannot be null");
            }

            var existingClient = Db.Clients.FirstOrDefault(x => x.Id == clientBOL.Id);

            if (existingClient == null)
            {
                throw new KeyNotFoundException($"Client with ID {clientBOL.Id} not found.");
            }

           //[MB!] - Pas besoin de faire ça, vous pouvez directement faire
                    //base.SaveEntity(clientBOL); 
                    //Revoir CowMilkEntityDAL

            existingClient.IsMember = clientBOL.IsMember;
            existingClient.LastName = clientBOL.LastName;
            existingClient.FirstName = clientBOL.FirstName;
            existingClient.Birthdate = clientBOL.Birthdate;
            existingClient.PhoneNumber = clientBOL.PhoneNumber;
            existingClient.Email = clientBOL.Email;
            existingClient.IdGenderDenomination = clientBOL.IdGenderDenomination;
            existingClient.Address = clientBOL.Address;
            existingClient.ZipCode = clientBOL.ZipCode;
            existingClient.IdMaritalStatus = clientBOL.IdMaritalStatus;
            existingClient.IdFamilySituation = clientBOL.IdFamilySituation;
            existingClient.AdultsAtHome = clientBOL.AdultsAtHome;
            existingClient.ChildsAtHome = clientBOL.ChildsAtHome;
            existingClient.IdHabitationType = clientBOL.IdHabitationType;
            existingClient.IdBank = clientBOL.IdBank;
            existingClient.IdEmploymentSituation = clientBOL.IdEmploymentSituation;
            existingClient.IdScholarshipType = clientBOL.IdScholarshipType;
            existingClient.Income = clientBOL.Income;
            existingClient.LastModifiedDate = DateTime.Now;

            Db.SaveChanges();
        }
    }
}
