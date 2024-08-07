using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataModels.BOL.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BOL.Production
{
    public class ProductionTypeDepartmentBOL : AbstractBOL<ProductionTypeDepartments>, IProductionTypeDepartmentBOL
    {
        public ProductionTypeDepartmentBOL() { }
        public ProductionTypeDepartmentBOL(ProductionTypeDepartments record) : base(record) { }

        public int ID { get { return Record.Id;} set { Record.Id = value; } }
        public string Name { get { return Record.Name; } set { Record.Name = value; } }
        
        public int IdProductionType { get { return Record.IdProductionType; } set { Record.IdProductionType = value; } }

        public string DisplayName { get { return Record.DisplayName; } set { Record.DisplayName = value; } }

        public int PurchaseVisualOrder { get { return Record.PurchaseVisualOrder; } set { Record.PurchaseVisualOrder = value; } }

        public bool HasRecipeDetailedDescrption { get { return Record.HasRecipeDetailedDescrption; } set { Record.HasRecipeDetailedDescrption = value; } }

        public string AmoutUnitDisplayName { get { return Record.AmoutUnitDisplayName; } set { Record.AmoutUnitDisplayName = value; } }

        public string ShortDisplayName { get { return Record.ShortDisplayName; } set { Record.ShortDisplayName = value; } }

    }
}
