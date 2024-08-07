using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Providers.Entity
{
    public class EmployeePermissionsEntityDAL : AcefEntityDAL, IEmployeePermissionsDAL
    {
        public EmployeePermissionsEntityDAL() { }
        public EmployeePermissionsEntityDAL(AcefEntityDAL externalDal) : base(externalDal) { }

        public IEnumerable<EmployeePermissions> GetAllPermissions()
        {
            return Db.EmployeePermissions.ToList();
        }

        public EmployeePermissions GetPermissionById(int id)
        {
            return Db.EmployeePermissions.Find(id);
        }

        public void CreatePermission(EmployeePermissions permission)
        {
            Db.EmployeePermissions.Add(permission);
            Db.SaveChanges();
        }

        public void UpdatePermission(EmployeePermissions permission)
        {
            Db.EmployeePermissions.Update(permission);
            Db.SaveChanges();
        }

        public void DeletePermission(int id)
        {
            var permission = Db.EmployeePermissions.Find(id);
            if (permission != null)
            {
                Db.EmployeePermissions.Remove(permission);
                Db.SaveChanges();
            }
        }
    }
}
