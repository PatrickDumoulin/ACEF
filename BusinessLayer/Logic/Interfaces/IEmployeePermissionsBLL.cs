using BusinessLayer.Communication.Responses.Common;
using CoreLib.Definitions;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Logic.Interfaces
{
    public interface IEmployeePermissionsBLL: IBLL
    {
        GetListResponse<EmployeePermissions> GetAllPermissions();
        GetItemResponse<EmployeePermissions> GetPermissionById(int id);
        void CreatePermission(EmployeePermissions permission);
        void UpdatePermission(EmployeePermissions permission);
        void DeletePermission(int id);
    }
}
