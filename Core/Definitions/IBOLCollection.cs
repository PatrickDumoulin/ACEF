using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Definitions
{
    public interface IBOLCollection
    {
        IEnumerable<IBOL> GetBolList();
    }
}
