using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Definitions
{
    public interface ILastModifiedTimeStampedRecord
    {
        DateTime LastModifiedDate { get; set; }
    }
}
