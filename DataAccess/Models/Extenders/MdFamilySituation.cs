using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public partial class MdFamilySituation : AbstractEntity
    {
        public string SequenceName { get { return "seqMasterData"; } }
    }
}
