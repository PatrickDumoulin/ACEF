using BusinessLayer.Communication.Responses.Common;
using CoreLib.Definitions;
using DataModels.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic.Interfaces
{
    public interface ICowMilkBLL : IBLL
    {
        GetItemResponse<ICowMilkIngredientBOL> GetIngredient(int id);
        GenericResponse SaveIngredient(ICowMilkIngredientBOL cowMilkIngredientBOL);

        GetItemResponse<ICowMilkIngredientPriceListMonthBOL> GetIngredientPriceListMonth(int id);
        GetListResponse<ICowMilkIngredientPriceListMonthBOL> GetIngredientPriceListMonthsFromPriceList(int idPriceList);
        GenericResponse SaveIngredientPriceListMonth(ICowMilkIngredientPriceListMonthBOL bol);
    }
}
