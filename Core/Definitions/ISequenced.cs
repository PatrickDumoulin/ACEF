using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Definitions
{
    public interface ISequenced : IIdentifierEntity
    {
        string SequenceName { get; }
    }
}
