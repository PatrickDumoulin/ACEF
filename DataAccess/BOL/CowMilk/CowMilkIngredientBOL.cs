using CoreLib.Definitions;
using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataModels.BOL;
using DataModels.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BOL.CowMilk
{
    public class CowMilkIngredientBOL : AbstractBOL<CowMilkIngredients>, ICowMilkIngredientBOL
    {
        #region Constructors
        public CowMilkIngredientBOL() : base() { }
        public CowMilkIngredientBOL(CowMilkIngredients record) : base(record) { }
        #endregion

        #region Properties
        public int Id { get { return Record.Id; } }

        public int IdProductionTypeDepartment { get { return Record.IdProductionTypeDeparment; } set { Record.IdProductionTypeDeparment = value; } }

        public string Name { get { return Record.Name; } set { Record.Name = value; } }

        public string DisplayName { get { return Record.DisplayName; } set { Record.DisplayName = value; } }

        public string UnitDisplayName { get { return Record.UnitDisplayName; } set { Record.UnitDisplayName = value; } }

        public decimal DefaultDcperTon { get { return Record.DefaultDcperTon; } set { Record.DefaultDcperTon = value; } }

        public ChildBols<ICowMilkIngredientPriceListMonthBOL> PriceLists
        {
            get
            {
                return GetSubBols<ICowMilkIngredientPriceListMonthBOL, CowMilkIngredientPriceListMonthBOL>(Record.CowMilkIngredientPriceListMonths.ToList<IRecord>());
            }
        }
        #endregion

    }
}
