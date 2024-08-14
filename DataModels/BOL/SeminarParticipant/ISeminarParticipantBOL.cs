using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.BOL.SeminarParticipant
{
    public interface ISeminarParticipantBOL : IBOL
    {
        public int Id { get; }

        public int? IdSeminar { get; set; }

        public int? IdClient { get; set; }

        public string LastName { get; set; }

        public string FirstName { get; set; }
    }
}
