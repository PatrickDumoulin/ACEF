using DataAccess.Core.Definitions;
using DataModels.BOL;
using DataModels.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BOL.CowMilk
{
    public class CowMilkIngredientMockBOL : AbstractUnboundBOL, ICowMilkIngredientBOL
    {
        public CowMilkIngredientMockBOL()
        {
            Id = GetNewSequence();
        }
        public CowMilkIngredientMockBOL(int id)
        {
            Id = id;
        }

        #region Properties
        public int Id { get; protected set; }
        public int IdProductionTypeDepartment { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string UnitDisplayName { get; set; }

        public decimal DefaultDcperTon { get; set; }

        public ChildBols<ICowMilkIngredientPriceListMonthBOL> PriceLists
        {
            get
            {
                return new ChildBols<ICowMilkIngredientPriceListMonthBOL>(null);
            }
        }
        #endregion
    }
}
