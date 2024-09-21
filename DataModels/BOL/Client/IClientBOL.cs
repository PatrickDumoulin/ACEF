using CoreLib.Definitions;
using DataModels.BOL.ClientIncomeType;
using DataModels.BOL.InterventionsInterventionSolutions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.BOL.Client
{
    /*[MB!] - Je constate que vous avez fait des interfaces pour chacun de vos BOLs, ce qui est très bien. Ceci dit, pourquoi ne vous en servez-vous pas dans les BLL? 
              Vous utilisez les classes concrètes dans le BLL alors qu'on devrait voir juste les interfaces.
     */
	public interface IClientBOL : IBOL
	{
		public int Id { get; set; }

		public bool IsMember { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime? Birthdate { get; set; }

        public string PhoneNumber { get; set; }

        public string Email { get; set; }

        public int? IdGenderDenomination { get; set; }

        public string Address { get; set; }

        public string ZipCode { get; set; }

        public int? IdMaritalStatus { get; set; }

        public int? IdFamilySituation { get; set; }

        public int? AdultsAtHome { get; set; }

        public int? ChildsAtHome { get; set; }

        public int? IdHabitationType { get; set; }

        public int? IdBank { get; set; }

        public int? IdEmploymentSituation { get; set; }

        public int? IdScholarshipType { get; set; }

        public byte[] Income { get; set; }

        public DateTime? CreatedDate { get; set; }

        public DateTime? LastModifiedDate { get; set; }
        public string FullName => $"{FirstName} {LastName}";

        IEnumerable<int> ClientIncomeTypesIds { get; }

        ICollection<IClientIncomeTypeBOL> ClientIncomeTypes { get; }
    }
}

