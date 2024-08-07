using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Core.Definitions;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.Providers.Entity;
using DataAccess.Providers.Interfaces;
using DataModels.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic
{
    public class CowMilkBLL : AbstractBLL<ICowMilkDAL>, ICowMilkBLL
    {
        #region Constructors
        public CowMilkBLL() { }
        public CowMilkBLL(ProviderDALTypes dalType) : base(dalType) { }
        public CowMilkBLL(IDAL externalDAL) : base(externalDAL) { }
        #endregion

        public GetItemResponse<ICowMilkIngredientBOL> GetIngredient(int id)
        {
            try
            {
                ICowMilkIngredientBOL bol = base.dal.GetIngredient(id);
                return new GetItemResponse<ICowMilkIngredientBOL>(bol);
            }
            catch (Exception ex) 
            {
                //base.ErrorLogger.LogError(ex);
                return new GetItemResponse<ICowMilkIngredientBOL>(ex);
            }
        }

        public GenericResponse SaveIngredient(ICowMilkIngredientBOL cowMilkIngredientBOL)
        {
            int result = base.dal.SaveIngredient(cowMilkIngredientBOL);
            return new GenericResponse(result != 0);
        }

        public GenericResponse SaveIngredientPriceListMonth(ICowMilkIngredientPriceListMonthBOL bol)
        {
            int result = base.dal.SaveIngredientPriceListMonth(bol);
            return new GenericResponse(result != 0);
        }

        public GetItemResponse<ICowMilkIngredientPriceListMonthBOL> GetIngredientPriceListMonth(int id)
        {
            ICowMilkIngredientPriceListMonthBOL bol = base.dal.GetIngredientPriceListMonth(id);
            return new GetItemResponse<ICowMilkIngredientPriceListMonthBOL>(bol);
        }

        public GetListResponse<ICowMilkIngredientPriceListMonthBOL> GetIngredientPriceListMonthsFromPriceList(int idPriceList)
        {
            List<ICowMilkIngredientPriceListMonthBOL> bols = base.dal.GetIngredientMonthlyPriceLists(idPriceList);
            return new GetListResponse<ICowMilkIngredientPriceListMonthBOL>(bols);
        }
    }
}
