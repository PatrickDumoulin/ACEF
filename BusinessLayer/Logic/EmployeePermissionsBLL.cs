using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Core.Definitions;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic
{
    public class EmployeePermissionsBLL : AbstractBLL<IEmployeePermissionsDAL>, IEmployeePermissionsBLL
    {
        public EmployeePermissionsBLL() { }
        public EmployeePermissionsBLL(ProviderDALTypes dalType) : base(dalType) { }
        public EmployeePermissionsBLL(IDAL externalDAL) : base(externalDAL) { }

        public GetListResponse<EmployeePermissions> GetAllPermissions()
        {
            var permissions = base.dal.GetAllPermissions();
            return new GetListResponse<EmployeePermissions>(permissions.ToList());
        }

        public GetItemResponse<EmployeePermissions> GetPermissionById(int id)
        {
            var permission = base.dal.GetPermissionById(id);
            return new GetItemResponse<EmployeePermissions>(permission);
        }

        public void CreatePermission(EmployeePermissions permission)
        {
            base.dal.CreatePermission(permission);
        }

        public void UpdatePermission(EmployeePermissions permission)
        {
            base.dal.UpdatePermission(permission);
        }

        public void DeletePermission(int id)
        {
            base.dal.DeletePermission(id);
        }
    }
}
