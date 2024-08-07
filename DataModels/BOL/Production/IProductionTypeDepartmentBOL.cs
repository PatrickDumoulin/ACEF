using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.BOL.Production
{
    public interface IProductionTypeDepartmentBOL : IBOL
    {
        int ID { get; set;}
        string Name { get; set; }
        int IdProductionType { get; set; }
        string DisplayName { get; set; }
        int PurchaseVisualOrder { get; set; }
        bool HasRecipeDetailedDescrption { get; set; }
        string AmoutUnitDisplayName { get; set; }
        string ShortDisplayName { get; set; }
    }
}
