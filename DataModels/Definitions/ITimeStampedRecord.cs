using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Definitions
{
    /// <summary>
    /// Defines a model that has time stamps for creation and edition
    /// </summary>
    public interface ITimeStampedRecord : ICreatedTimeStampedRecord, ILastModifiedTimeStampedRecord
    {

    }
}
