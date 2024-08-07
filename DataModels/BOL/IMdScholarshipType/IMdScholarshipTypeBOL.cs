using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.BOL.IMdScholarshipType
{
    public interface IMdScholarshipTypeBOL : IBOL
    {
        public int Id { get; }

        public string Name { get; set; }

        public bool? Active { get; set; }
    }
}
