using DataAccess.Core.Definitions;
using DataModels.BOL.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BOL.Client
{
    public class ClientMockBOL : AbstractUnboundBOL, IClientBOL
    {
        public ClientMockBOL()
        {
            Id = GetNewSequence();
        }

        public ClientMockBOL(int id)
        {
            Id = id;
        }




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

        private int GetNewSequence()
        {
            // Generate a new sequence ID for mock purposes
            return new Random().Next(1, 1000);
        }

    }

}
