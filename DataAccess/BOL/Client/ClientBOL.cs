
using DataAccess.BOL.ClientIncomeType;
using DataAccess.BOL.InterventionsInterventionSolutions;
using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataModels.BOL.Client;
using DataModels.BOL.ClientIncomeType;
using DataModels.BOL.InterventionsInterventionSolutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BOL.Client
{
	public class ClientBOL : AbstractBOL<Clients>, IClientBOL
	{
		public ClientBOL() { }
		public ClientBOL(Clients record) : base(record) { }


        public int Id { get { return base.Record.Id; } set { base.Record.Id = value; } }
        public bool IsMember { get { return base.Record.IsMember; } set { base.Record.IsMember = value; } }
        public string LastName { get { return base.Record.LastName; } set { base.Record.LastName = value; } }
        public string FirstName { get { return base.Record.FirstName; } set { base.Record.FirstName = value; } }
        public DateTime? Birthdate { get { return base.Record.Birthdate; } set { base.Record.Birthdate = value; } }
        public string PhoneNumber { get {  return base.Record.PhoneNumber ?? "Non spécifié"; } set { base.Record.PhoneNumber = value; } }
        public string Email { get { return base.Record.Email ?? "Non spécifié"; } set { base.Record.Email = value; } }
        public int? IdGenderDenomination { get { return base.Record.IdGenderDenomination; } set { base.Record.IdGenderDenomination = value; } }
        public string Address { get { return base.Record.Address ?? "Non spécifié"; } set { base.Record.Address = value; } }
        public string ZipCode { get { return base.Record.ZipCode ?? "Non spécifié"; } set { base.Record.ZipCode = value; } }
        public int? IdMaritalStatus { get { return base.Record.IdMaritalStatus; } set { base.Record.IdMaritalStatus = value; } }
        public int? IdFamilySituation { get { return base.Record.IdFamilySituation; } set { base.Record.IdFamilySituation = value; } }
        public int? AdultsAtHome { get { return base.Record.AdultsAtHome; } set { base.Record.AdultsAtHome = value; } }
        public int? ChildsAtHome { get { return base.Record.ChildsAtHome; } set { base.Record.ChildsAtHome = value; } }
        public int? IdHabitationType { get { return base.Record.IdHabitationType; } set { base.Record.IdHabitationType = value; } }
        public int? IdBank { get { return base.Record.IdBank; } set { base.Record.IdBank = value; } }
        public int? IdEmploymentSituation { get { return base.Record.IdEmploymentSituation; } set { base.Record.IdEmploymentSituation = value; } }
        public int? IdScholarshipType { get { return base.Record.IdScholarshipType; } set { base.Record.IdScholarshipType = value; } }
        public byte[] Income { get { return base.Record.Income; } set { base.Record.Income = value; } }
        public DateTime? CreatedDate { get { return base.Record.CreatedDate; } set { base.Record.CreatedDate = value; } }
        public DateTime? LastModifiedDate { get { return base.Record.LastModifiedDate; } set { base.Record.LastModifiedDate = value; } }

        public string FullName => $"{FirstName} {LastName}";

        // Propriété pour les IDs des solutions d'intervention
        public IEnumerable<int> ClientIncomeTypesIds
        {
            get
            {
                return (IEnumerable<int>)base.Record.ClientsIncomeTypes
                .Select(x => x.IdIncomeType);
            }
        }

        // Propriété pour la collection des income Types
        public ICollection<IClientIncomeTypeBOL> ClientIncomeTypes
        {
            get
            {
                return base.Record.ClientsIncomeTypes
                    .Select(x => new ClientIncomeTypeBOL(x))
                    .Cast<IClientIncomeTypeBOL>()
                    .ToList();
            }
            set
            {
                base.Record.ClientsIncomeTypes = value.Select(x => new ClientsIncomeTypes
                {
                    IdIncomeType = x.IdIncomeType,
                    IdClient = this.Id
                }).ToList();
            }
        }
    }
}
