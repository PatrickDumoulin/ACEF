using CoreLib.Definitions;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Interfaces
{
    public interface IEmployeePermissionsDAL : IDAL
    {
        IEnumerable<EmployeePermissions> GetAllPermissions();
        EmployeePermissions GetPermissionById(int id);
        void CreatePermission(EmployeePermissions permission);
        void UpdatePermission(EmployeePermissions permission);
        void DeletePermission(int id);
    }
}
