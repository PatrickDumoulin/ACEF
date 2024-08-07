using CoreLib.Definitions;
using DataAccess.BOL;
using DataAccess.BOL.CowMilk;
using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using DataModels.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Mock
{
    public class CowMilkMockDAL : AcefMockDAL, ICowMilkDAL
    {
        public ICowMilkIngredientBOL GetIngredient(int id) 
        {
            ICowMilkIngredientBOL bol = new CowMilkIngredientMockBOL(id);
            bol.Name = "MockName";
            bol.UnitDisplayName = "MockData";
            bol.DefaultDcperTon = 3;
            bol.DisplayName = "Mock";
            bol.IdProductionTypeDepartment = 3;

            return bol;
        }

        public int SaveIngredient(ICowMilkIngredientBOL bol)
        {
            return base.SaveEntity(bol);
        }

        public int SaveIngredientPriceListMonth(ICowMilkIngredientPriceListMonthBOL bol)
        {
            return base.SaveEntity(bol);
        }

        public ICowMilkIngredientPriceListMonthBOL GetIngredientPriceListMonth(int id)
        {
            throw new NotImplementedException();
        }

        public List<ICowMilkIngredientPriceListMonthBOL> GetIngredientMonthlyPriceLists(int idPriceList)
        {
            throw new NotImplementedException();
        }
    }
}
