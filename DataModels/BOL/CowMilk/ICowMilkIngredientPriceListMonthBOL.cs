using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.BOL
{
    public interface ICowMilkIngredientPriceListMonthBOL : IBOL
    {
        public int Id { get; }

        public int IdCowMilkIngredientPriceList { get; set; }
        public int IdCowMilkIngredient { get; set; }

        public decimal Amount { get; set; }
        public decimal DCPercentage { get; set; }

        public DateTime EffectiveMonthDate { get; set; }
        public DateTime CreatedDate { get; }
        public DateTime LastModifiedDate { get; }
    }
}
