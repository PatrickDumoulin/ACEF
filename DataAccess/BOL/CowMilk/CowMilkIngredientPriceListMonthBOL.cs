using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataModels.BOL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BOL.CowMilk
{
    public class CowMilkIngredientPriceListMonthBOL : AbstractBOL<CowMilkIngredientPriceListMonths>, ICowMilkIngredientPriceListMonthBOL
    {
        #region Constructors
        public CowMilkIngredientPriceListMonthBOL() : base() { }
        public CowMilkIngredientPriceListMonthBOL(CowMilkIngredientPriceListMonths record) : base(record) { }
        #endregion

        #region Properties
        public int Id { get { return Record.Id; } }

        public int IdCowMilkIngredientPriceList { get { return Record.IdCowMilkIngredientPriceList; } set { Record.IdCowMilkIngredientPriceList = value; } }
        public int IdCowMilkIngredient { get { return Record.IdCowMilkIngredient; } set { Record.IdCowMilkIngredient = value; } }

        public decimal Amount { get { return base.DecryptNumericValue(Record.Amount); } set { Record.Amount = base.EncryptNumericValue(value); } }
        public decimal DCPercentage { get { return Record.Dcpercentage; } set { Record.Dcpercentage = value; } }

        public DateTime EffectiveMonthDate { get { return Record.EffectiveMonthDate; } set { Record.EffectiveMonthDate = value; } }
        public DateTime CreatedDate { get { return Record.CreatedDate; } }
        public DateTime LastModifiedDate { get { return Record.LastModifiedDate; } }
        #endregion
    }
}
