using CoreLib.Definitions;
using DataModels.Definitions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public partial class Seminars : IRecord, ICreatedTimeStampedRecord
    {
        [NotMapped]
        public ICollection<IRecord> LoadedRecords { get; private set; }
        DateTime ICreatedTimeStampedRecord.CreatedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Seminars()
        {
            LoadedRecords = new List<IRecord>();
        }
    }
}
