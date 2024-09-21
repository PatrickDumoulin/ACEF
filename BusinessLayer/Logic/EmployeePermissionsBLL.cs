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

        //[MB] - Pour suivre votre nomenclature, le All n'est pas nécessaire. On s'en doute avec une méthode n'offrant pas de paramètres.
        public GetListResponse<EmployeePermissions> GetAllPermissions()
        {
            var permissions = base.dal.GetAllPermissions();
            return new GetListResponse<EmployeePermissions>(permissions.ToList());
        }

        //[MB] - Modifier pour GetPermission(int id)
        public GetItemResponse<EmployeePermissions> GetPermissionById(int id)
        {
            var permission = base.dal.GetPermissionById(id);
            return new GetItemResponse<EmployeePermissions>(permission);
        }

        public void CreatePermission(EmployeePermissions permission)
        {
            //[MB] - Validations?
            base.dal.CreatePermission(permission);
        }

        public void UpdatePermission(EmployeePermissions permission)
        {
            //[MB] - Validations?
            base.dal.UpdatePermission(permission);
        }

        public void DeletePermission(int id)
        {
            base.dal.DeletePermission(id);
        }
    }
}
