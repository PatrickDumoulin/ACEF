using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.BOL.Client
{
	public interface IClientBOL : IBOL
	{
		public int Id { get; }

		public bool IsMember { get; }

		public string LastName { get; }

		public string FirstName { get; }

		public DateTime? Birthdate { get; }

		public string PhoneNumber { get; }

		public string Email { get; }

		public int? IdGenderDenomination { get; }

		public string Address { get; }

		public string ZipCode { get; }

		public int? IdMaritalStatus { get; }

		public int? IdFamilySituation { get; }

		public int? AdultsAtHome { get; }

		public int? ChildsAtHome { get; }

		public int? IdHabitationType { get; }

		public int? IdBank { get; }

		public int? IdEmploymentSituation { get; }

		public int? IdScholarshipType { get; }

		public byte[] Income { get; }

		public DateTime? CreatedDate { get; }

		public DateTime? LastModifiedDate { get; }
        public string FullName => $"{FirstName} {LastName}";
    }
}

