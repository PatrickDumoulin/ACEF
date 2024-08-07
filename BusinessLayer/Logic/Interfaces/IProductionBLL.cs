using BusinessLayer.Communication.Responses.Common;
using CoreLib.Definitions;
using DataModels.BOL.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic.Interfaces
{
    public interface IProductionBLL : IBLL
    {
        GetListResponse<IProductionTypeBOL> GetProductionTypes();
        GetItemResponse<IProductionTypeDepartmentBOL> GetProductionTypeDepartment(int id);
    }
}
