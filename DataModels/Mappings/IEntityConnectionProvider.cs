using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.Mappings
{
    public interface IEntityConnectionProvider
    {
        string ServerName { get; set; }
        string DatabaseName { get; set; }
    }
}
