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
    public class ProductionTypeBOL : AbstractBOL<ProductionTypes>, IProductionTypeBOL
    {
        public ProductionTypeBOL() { }
        public ProductionTypeBOL(ProductionTypes record) : base(record) { }

        public int Id { get { return Record.Id; } }
        public string Name { get { return Record.Name; } set { Record.Name = value; } }
    }
}
