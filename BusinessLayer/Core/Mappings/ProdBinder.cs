using BusinessLayer.Logic;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Injection;
using DataAccess.Providers.Entity;
using DataAccess.Providers.Interfaces;
using DataAccess.Providers.Mock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Core.Mappings
{
    public class ProdBinder : BaseBinder
    {
        public override void Load()
        {
            #region BindBll
            BindBll<ICowMilkBLL, CowMilkBLL>();
            BindBll<ICowMilkBLL, CowMilkBLL>(CoreLib.Definitions.ProviderDALTypes.MOCK);

            BindBll<IProductionBLL, ProductionBLL>();
            BindBll<IClientBLL, ClientBLL>();
            BindBll<IMDBLL, MDBLL>();
            BindBll<INoteBLL, NoteBLL>();
            BindBll<IClientAttachmentBLL, ClientAttachmentBLL>();
            BindBll<ISeminarBLL, SeminarBLL>();
            BindBll<IEmployeePermissionsBLL, EmployeePermissionsBLL>();
            BindBll<IEmployeeBLL, EmployeeBLL>();
            BindBll<IInterventionBLL, InterventionBLL>();

            #endregion

            #region BindDal
            BindDal<ICowMilkDAL, CowMilkEntityDAL>();
            BindDal<ICowMilkDAL, CowMilkMockDAL>(CoreLib.Definitions.ProviderDALTypes.MOCK);

            BindDal<IProductionDAL, ProductionEntityDAL>();
            BindDal<IClientDAL, ClientEntityDAL>();
            BindDal<IMDDAL, MDEntityDAL>();
            BindDal<INoteDAL, NoteEntityDAL>();
            BindDal<IClientAttachmentDAL, ClientAttachmentEntityDAL>();
            BindDal<ISeminarDAL, SeminarEntityDAL>();
            BindDal<IEmployeePermissionsDAL, EmployeePermissionsEntityDAL>();
            BindDal<IEmployeeDAL, EmployeeEntityDAL>();
            BindDal<IInterventionDAL, InterventionEntityDAL>();

            #endregion

        }
    }
}
