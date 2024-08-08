using DataAccess.Core.Definitions;
using DataModels.BOL.MdSeminarThemes;
using System;

namespace DataAccess.BOL.MdSeminarThemes
{
    public class MdSeminarThemesBOL : AbstractBOL<Models.MdSeminarThemes>, IMdSeminarThemesBOL
    {
        public MdSeminarThemesBOL() { }

        public MdSeminarThemesBOL(Models.MdSeminarThemes record) : base(record) { }

        public int Id { get { return base.Record.Id; } }

        public string Name { get { return base.Record.Name; } set { base.Record.Name = value; } }

        public bool? Active { get { return base.Record.Active; } set { base.Record.Active = value; } }
    }
}
