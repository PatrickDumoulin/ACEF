using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.BOL.EmployeePermission
{
    public interface IEmployeePermissionsBOL : IBOL
    {
        int Id { get;  }
        int? EmployeeId { get;  }
        bool? AllowInterventions { get; }
        bool? AllowReports { get; set; }
        bool? AllowSu { get; set; }
    }
}
