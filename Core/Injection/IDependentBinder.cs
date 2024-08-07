using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Injection
{
    public interface IDependentBinder
    {
        BaseBinder ReferencedBinder { get; }
    }
}
