using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Core.Definitions;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.Providers.Interfaces;
using DataModels.BOL.Production;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic
{
    public class ProductionBLL : AbstractBLL<IProductionDAL>, IProductionBLL
    {
        #region Constructors
        public ProductionBLL() { }
        public ProductionBLL(ProviderDALTypes dalType) { }
        public ProductionBLL(IDAL dal) : base(dal) { }
        public ProductionBLL(IDAL dal, ProviderDALTypes dalType) : base(dal, dalType) { }
        #endregion

        public GetListResponse<IProductionTypeBOL> GetProductionTypes()
        {
            List<IProductionTypeBOL> bols = base.dal.GetProductionTypes();
            return new GetListResponse<IProductionTypeBOL>(bols);
        }

        public GetItemResponse<IProductionTypeDepartmentBOL> GetProductionTypeDepartment(int id)
        {
            IProductionTypeDepartmentBOL bol = base.dal.GetProductionTypeDepartment(id);
            return new GetItemResponse<IProductionTypeDepartmentBOL>(bol);
        }
    }
}
