using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.BOL.Seminar
{
    public interface ISeminarBOL : IBOL
    {
        int Id { get; }
        DateTime? DateSeminar { get; }
        int? IdSeminarTheme { get; }
        string Notes { get; }
        DateTime? CreatedDate { get; }

    }
}
