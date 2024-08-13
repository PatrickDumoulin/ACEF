using CoreLib.Definitions;
using DataModels.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public partial class ClientsAttachments : IRecord, ICreatedTimeStampedRecord
    {
        
        public ICollection<IRecord> LoadedRecords { get; } = new List<IRecord>();
        DateTime ICreatedTimeStampedRecord.CreatedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}