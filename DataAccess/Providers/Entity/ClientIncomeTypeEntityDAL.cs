using DataAccess.BOL.Client;
using DataAccess.BOL.ClientAttachment;
using DataAccess.BOL.ClientIncomeType;
using DataAccess.BOL.InterventionsInterventionSolutions;
using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Entity
{
    public class ClientIncomeTypeEntityDAL : AcefEntityDAL, IClientIncomeTypeDAL
    {
        public ClientIncomeTypeEntityDAL() { }
        public ClientIncomeTypeEntityDAL(AcefEntityDAL externalDal) : base(externalDal) { }


        public ClientIncomeTypeBOL GetClientIncomeTypeById(int id)
        {
            ClientsIncomeTypes record = Db.ClientsIncomeTypes.FirstOrDefault(x => x.Id == id);
            return MapperWrapper.NewBol<ClientIncomeTypeBOL>(record);
        }

        public List<ClientIncomeTypeBOL> GetClientIncomeTypesByClientId(int clientId)
        {
            var records = Db.ClientsIncomeTypes
                .Where(x => x.IdClient == clientId)
                .ToList();
            return records.Select(record => new ClientIncomeTypeBOL(record)).ToList();
        }

        public void CreateClientIncomeType(ClientIncomeTypeBOL clientIncomeTypeBOL)
        {
            if (clientIncomeTypeBOL == null)
            {
                throw new ArgumentNullException(nameof(InterventionsInterventionSolutionsBOL), "incomeTypeBOL cannot be null");
            }

            var newClientsIncomeTypes = new ClientsIncomeTypes
            {
                IdClient = clientIncomeTypeBOL.IdClient,
                IdIncomeType = clientIncomeTypeBOL.IdIncomeType,

            };

            Db.ClientsIncomeTypes.Add(newClientsIncomeTypes);
            Db.SaveChanges();
        }

        public void DeleteClientIncomeType(int id)
        {
            var clientIncomeType = Db.ClientsIncomeTypes.FirstOrDefault(x => x.Id == id);
            if (clientIncomeType != null)
            {
                Db.ClientsIncomeTypes.Remove(clientIncomeType);
                Db.SaveChanges();
            }
        }

        
    }
}
