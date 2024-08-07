using CoreLib.Definitions;
using DataModels.BOL.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Interfaces
{
    public interface IProductionDAL : IDAL
    {
        List<IProductionTypeBOL> GetProductionTypes();
        IProductionTypeDepartmentBOL GetProductionTypeDepartment(int id);
    }
}
