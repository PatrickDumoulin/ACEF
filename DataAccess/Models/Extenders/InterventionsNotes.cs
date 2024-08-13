using CoreLib.Definitions;
using DataModels.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Extenders
{
    public partial class InterventionsNotes : AbstractEntity, ICreatedTimeStampedRecord
    {

        public DateTime CreatedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
