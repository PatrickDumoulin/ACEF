using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Definitions
{
    public interface IBOL
    {
        ObjectState State { get; set; }

        Dictionary<Type, object> SubBols { get; }

        IRecord UntypedRecord { get; }
    }
}
