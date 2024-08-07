using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public partial class Seminars : IRecord
    {
        [NotMapped]
        public ICollection<IRecord> LoadedRecords { get; private set; }

        public Seminars()
        {
            LoadedRecords = new List<IRecord>();
        }
    }
}
