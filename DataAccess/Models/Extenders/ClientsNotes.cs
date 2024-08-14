using CoreLib.Definitions;
using DataModels.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models.Extenders
{
    public partial class ClientsNotes : AbstractEntity, ICreatedTimeStampedRecord
    {
        //public string SequenceName { get { return "Clients_note_Seq"; } }
        public DateTime CreatedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        
    }
}
