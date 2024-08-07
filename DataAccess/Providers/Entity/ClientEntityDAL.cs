using CoreLib.Definitions;
using DataAccess.BOL.Client;

using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using DataModels.BOL.Client;
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

        

        public IClientBOL GetClient(int id)
		{
			Clients record = Db.Clients.FirstOrDefault(x=> x.Id == id);
			return MapperWrapper.NewBol<ClientBOL>(record);
		}


        public List<IClientBOL> GetClients()
        {
            List<IRecord> records = Db.Clients.ToList<IRecord>();
            return MapperWrapper.NewBols<ClientBOL>(records).ToList<IClientBOL>();
        }

        public void CreateClient(IClientBOL clientBOL)
        {
            if (clientBOL == null)
            {
                throw new ArgumentNullException(nameof(clientBOL), "ClientBOL cannot be null");
            }

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
