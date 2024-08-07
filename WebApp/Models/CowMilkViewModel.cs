using DataModels.BOL;
using DataModels.BOL.Production;

namespace WebApp.Models
{
    public class CowMilkViewModel
    {
        public CowMilkViewModel() { }
        public CowMilkViewModel(ICowMilkIngredientBOL ingredient, IProductionTypeDepartmentBOL productionTypeDepartment) 
        { 
            this.IdCowMilkIngredient = ingredient.Id;
            this.ProductionDepartentName = productionTypeDepartment.Name;
            this.DefaultDcPerTon = ingredient.DefaultDcperTon;
        }

        public int IdCowMilkIngredient { get; set; }
        public string ProductionDepartentName { get; set; }
        public decimal DefaultDcPerTon { get; set; }
        
    }
}
