using CoreLib.Definitions;
using System;

namespace DataModels.BOL.MdIncomeType
{
    public interface IMdIncomeTypeBOL : IBOL
    {
        int Id { get; }
        string Name { get; set; }
        bool? Active { get; set; }
    }
}
