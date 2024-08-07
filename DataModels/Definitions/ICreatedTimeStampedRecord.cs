using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Definitions
{
    public interface ICreatedTimeStampedRecord
    {
        DateTime CreatedDate { get; set; }
    }
}
