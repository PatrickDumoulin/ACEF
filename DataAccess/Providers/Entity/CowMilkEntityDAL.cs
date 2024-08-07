using CoreLib.Definitions;
using DataAccess.BOL.CowMilk;
using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using DataModels.BOL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Entity
{
    public class CowMilkEntityDAL : AcefEntityDAL, ICowMilkDAL
    {
        public CowMilkEntityDAL() { }

        public ICowMilkIngredientBOL GetIngredient(int id)
        {
            CowMilkIngredients record = base.Db.CowMilkIngredients
                                              .Include(x => x.CowMilkIngredientPriceListMonths)
                                              .Where(x => x.Id == id).FirstOrDefault();

            return new CowMilkIngredientBOL(record);
        }

        public int SaveIngredient(ICowMilkIngredientBOL bol)
        {
            return base.SaveEntity(bol, true);
        }

        public int SaveIngredientPriceListMonth(ICowMilkIngredientPriceListMonthBOL bol)
        {
            return base.SaveEntity(bol);
        }

        public ICowMilkIngredientPriceListMonthBOL GetIngredientPriceListMonth(int id)
        {
            CowMilkIngredientPriceListMonths record = base.Db.CowMilkIngredientPriceListMonths
                                              .Where(x => x.Id == id).FirstOrDefault();

            return base.MapperWrapper.NewBol<CowMilkIngredientPriceListMonthBOL>(record);
        }
        public List<ICowMilkIngredientPriceListMonthBOL> GetIngredientMonthlyPriceLists(int idPriceList)
        {
            List<CowMilkIngredientPriceListMonths> records = base.Db.CowMilkIngredientPriceListMonths
                                                                   .Where(x => x.IdCowMilkIngredientPriceList == idPriceList).ToList();
          
            return base.MapperWrapper.NewBols<CowMilkIngredientPriceListMonthBOL>(records.ToList<IRecord>()).ToList<ICowMilkIngredientPriceListMonthBOL>();
        }
    }
}
