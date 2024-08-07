using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BOL.Note
{
    public class NoteBOL
    {
        public int Id { get; set; }
        public int? IdClient { get; set; }
        public int? IdEmployee { get; set; }
        public string Comment { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}
