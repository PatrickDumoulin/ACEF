using CoreLib.Definitions;
using DataModels.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Interfaces
{
    public interface ICowMilkDAL : IDAL
    {
        ICowMilkIngredientBOL GetIngredient(int id);
        int SaveIngredient(ICowMilkIngredientBOL bol);

        ICowMilkIngredientPriceListMonthBOL GetIngredientPriceListMonth(int id);
        List<ICowMilkIngredientPriceListMonthBOL> GetIngredientMonthlyPriceLists(int idPriceList);
        int SaveIngredientPriceListMonth(ICowMilkIngredientPriceListMonthBOL bol);
    }
}
