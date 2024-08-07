using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.BOL.Production
{
    public interface IProductionTypeBOL : IBOL
    {
        int Id { get; }
        string Name { get; set; }
    }
}
