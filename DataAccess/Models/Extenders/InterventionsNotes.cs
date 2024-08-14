using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public partial class InterventionsNotes : IRecord
    {
        public ICollection<IRecord> LoadedRecords { get; } = new List<IRecord>();
    }
}
