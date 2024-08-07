using CoreLib.Definitions;
using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataModels.BOL.Seminar;

namespace DataAccess.BOL.Seminar
{
    public class SeminarBOL : AbstractBOL<Seminars>, ISeminarBOL
    {
        public SeminarBOL() { }

        public SeminarBOL(Seminars record) : base(record) { }

        public SeminarBOL(DateTime? dateSeminar, int? idSeminarTheme, string notes, DateTime? createdDate)
            : base(new Seminars
            {
                DateSeminar = dateSeminar,
                IdSeminarTheme = idSeminarTheme,
                Notes = notes,
                CreatedDate = createdDate
            })
        {
        }

        public int Id { get { return base.Record.Id; } }
        public DateTime? DateSeminar { get { return base.Record.DateSeminar; } }
        public int? IdSeminarTheme { get { return base.Record.IdSeminarTheme; } }
        public string Notes { get { return base.Record.Notes; } }
        public DateTime? CreatedDate { get { return base.Record.CreatedDate; } }
        
        public List<Clients> Participants { get; set; } = new List<Clients>();
    }
}
