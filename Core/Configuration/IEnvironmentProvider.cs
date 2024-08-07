using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Configuration
{
    public interface IEnvironmentProvider
    {
        string Name { get; }
    }
}
