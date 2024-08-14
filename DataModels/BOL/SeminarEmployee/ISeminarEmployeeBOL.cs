using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.BOL.SeminarEmployee
{
    public interface ISeminarEmployeeBOL : IBOL
    {
        public int Id { get; }

        public int? IdSeminar { get; set; }

        public int? IdEmployee { get; set; }
    }
}
