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
    public partial class InterventionsAttachments : AbstractEntity, IRecord//ICreatedTimeStampedRecord
    {
        [NotMapped]
        public ICollection<IRecord> LoadedRecords { get; } = new List<IRecord>();
        //public DateTime CreatedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
