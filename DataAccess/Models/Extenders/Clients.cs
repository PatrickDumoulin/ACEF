using CoreLib.Definitions;
using DataModels.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
	public partial class Clients : AbstractEntity, ISequenced
	{
		public string SequenceName { get { return "Clients_Seq"; } }
	}
}
