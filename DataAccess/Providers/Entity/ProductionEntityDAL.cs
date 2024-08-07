using CoreLib.Definitions;
using DataAccess.BOL.CowMilk;
using DataAccess.BOL.Production;
using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using DataModels.BOL;
using DataModels.BOL.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Entity
{
    public class ProductionEntityDAL : AcefEntityDAL, IProductionDAL
    {
        public ProductionEntityDAL() { }
        public ProductionEntityDAL(AcefEntityDAL externalDal) : base(externalDal) { }

        public List<IProductionTypeBOL> GetProductionTypes()
        {
            List<ProductionTypes> records = base.Db.ProductionTypes.ToList();                                        
            return base.MapperWrapper.NewBols<ProductionTypeBOL>(records.ToList<IRecord>()).ToList<IProductionTypeBOL>();
        }

        public IProductionTypeDepartmentBOL GetProductionTypeDepartment(int id)
        {
            ProductionTypeDepartments record = base.Db.ProductionTypeDepartments.FirstOrDefault(x => x.Id == id);
            return base.MapperWrapper.NewBol<ProductionTypeDepartmentBOL>(record);
        }
    }
}
