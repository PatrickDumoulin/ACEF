using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BOL.Note
{
    //[MB] - Il y a une raison pourquoi ce BOL est détaché de la BD? 
    public class NoteBOL
    {
        public int Id { get; set; }
        public int? IdClient { get; set; }
        public int? IdEmployee { get; set; }
        public string Comment { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
