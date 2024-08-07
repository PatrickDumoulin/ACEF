using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreLib.Definitions
{
    public interface IBLL
    {
        U GetBLL<U>() where U : IBLL;
        U GetBLL<U>(ProviderDALTypes dalType) where U : IBLL;
    }
}
