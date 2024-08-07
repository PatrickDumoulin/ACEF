using CoreLib.Definitions;
using DataModels.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.BOL
{
    public interface ICowMilkIngredientBOL : IBOL
    {
        int Id { get; }
        int IdProductionTypeDepartment { get; set; }
        string Name { get; set; }
        string DisplayName { get; set; }
        string UnitDisplayName { get; set; }

        decimal DefaultDcperTon { get; set; }

        ChildBols<ICowMilkIngredientPriceListMonthBOL> PriceLists { get; }
    }
}
