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

        //[MB!] - Pourquoi? Ils n'ont pas besoin d'être défini ici. De plus, pourquoi tous avoir mis les interfaces de gestion de temps?
        //ICreatedTimeStampedRecord : L'entité gère juste un CreatedDate
        //ILastModifiedTimeStampedRecord : L'entité gère juste un LastModifiedDate
        //ITimeStampedRecord : L'entité gère les deux
        DateTime ICreatedTimeStampedRecord.CreatedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DateTime ILastModifiedTimeStampedRecord.LastModifiedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
