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

        //[MB] - Je ne pense pas que cela fonctionne? Votre record que vous créer va avoir un ID à 0. Quand on veut créer un nouveau BOL on passe par le constructeur sans paramètre parce que
        //celui-ci s'occupe de fetcher le prochain ID de la BD et l'assigne automatiquement à votre record. Ici vous faites un shortcut qui est dangereux.
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

        // Ajout des setters pour permettre l'assignation
        public int Id
        {
            get { return base.Record.Id; }
            set { base.Record.Id = value; }  // Ajoutez un setter ici pour permettre l'assignation
        }

        public DateTime? DateSeminar
        {
            get { return base.Record.DateSeminar; }
            set { base.Record.DateSeminar = value; }  // Ajout du setter
        }

        public int? IdSeminarTheme
        {
            get { return base.Record.IdSeminarTheme; }
            set { base.Record.IdSeminarTheme = value; }  // Ajout du setter
        }

        public string Notes
        {
            get { return base.Record.Notes; }
            set { base.Record.Notes = value; }  // Ajout du setter
        }

        public DateTime? CreatedDate
        {
            get { return base.Record.CreatedDate; }
            set { base.Record.CreatedDate = value; }  // Ajout du setter (facultatif si nécessaire)
        }

        public List<Clients> Participants { get; set; } = new List<Clients>();
        public List<Employees> Intervenants { get; set; } = new List<Employees>();
    }
}
