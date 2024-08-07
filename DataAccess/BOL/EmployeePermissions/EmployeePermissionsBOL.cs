using CoreLib.Definitions;
using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataModels.BOL.EmployeePermission;


namespace DataAccess.BOL.EmployeePermission
{
    public class EmployeePermissionsBOL : AbstractBOL<EmployeePermissions>, IEmployeePermissionsBOL
    {
        public EmployeePermissionsBOL() { }

        public EmployeePermissionsBOL(EmployeePermissions record) : base(record) { }

        public int Id => base.Record.Id;
        public int? EmployeeId => base.Record.EmployeeId;
        public bool? AllowInterventions => base.Record.AllowInterventions;
        public bool? AllowReports { get => base.Record.AllowReports; set => base.Record.AllowReports = value; }
        public bool? AllowSu { get => base.Record.AllowSu; set => base.Record.AllowSu = value; }
    }
}
