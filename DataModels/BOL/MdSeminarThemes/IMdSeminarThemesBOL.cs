using CoreLib.Definitions;
using System;

namespace DataModels.BOL.MdSeminarThemes
{
    public interface IMdSeminarThemesBOL : IBOL
    {
        int Id { get; }
        string Name { get; set; }
        bool? Active { get; set; }
    }
}
