using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Definitions
{
    public abstract class AbstractEntity : IRecord
    {
        public AbstractEntity()
        {
            this.LoadedRecords = new List<IRecord>();
        }

        [NotMapped]
        public ICollection<IRecord> LoadedRecords { get; private set; }
    }
}
