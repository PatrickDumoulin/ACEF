using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Definitions
{
    public enum ObjectState
    {
        LOADED = 0,
        ADDED = 1,
        MODIFIED = 2,
        DELETED = 3,
        DETACHED = 4
    }
}
