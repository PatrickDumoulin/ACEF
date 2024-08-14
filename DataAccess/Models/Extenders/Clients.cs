using CoreLib.Definitions;
using DataModels.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public partial class Clients : AbstractEntity, ISequenced, ICreatedTimeStampedRecord, ILastModifiedTimeStampedRecord, ITimeStampedRecord
    {
		public string SequenceName { get { return "Clients_Seq"; } }

        DateTime ICreatedTimeStampedRecord.CreatedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DateTime ILastModifiedTimeStampedRecord.LastModifiedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
