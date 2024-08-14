using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataModels.BOL.Seminar;
using DataModels.BOL.SeminarParticipant;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BOL.SeminarParticipant
{
    public class SeminarParticipantBOL : AbstractBOL<SeminarsParticipants>, ISeminarParticipantBOL
    {
        public SeminarParticipantBOL() { }
        public SeminarParticipantBOL(SeminarsParticipants record) : base(record) { }

        public int Id { get { return base.Record.Id; } }

        public int? IdSeminar { get { return base.Record.IdSeminar; } set { base.Record.IdSeminar = value; } }

        public int? IdClient { get { return base.Record.IdClient; } set { base.Record.IdClient = value; } }

        public string LastName { get { return base.Record.LastName; } set { base.Record.LastName = value; } }

        public string FirstName { get { return base.Record.FirstName; } set { base.Record.FirstName = value; } }
    }
}
