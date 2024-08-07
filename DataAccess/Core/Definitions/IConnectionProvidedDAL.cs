using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Core.Definitions
{
    public interface IConnectionProvidedDAL : IDAL
    {
        string ConnectionString { get; }
    }
}
