using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataModels.BOL.Employe;
using DataModels.BOL.EmployeePermission;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.BOL.Employe
{
    public class EmployeeBOL : AbstractBOL<Employees>, IEmployeeBOL
    {
        public EmployeeBOL() { }

        public EmployeeBOL(Employees record) : base(record) { }

        public int Id => base.Record.Id;
        public string FirstName { get => base.Record.FirstName; set => base.Record.FirstName = value; }
        public string LastName { get => base.Record.LastName; set => base.Record.LastName = value; }
        public string UserName { get => base.Record.UserName; set => base.Record.UserName = value; }
        public string PasswordHash { get => base.Record.PasswordHash; set => base.Record.PasswordHash = value; }
        public DateTime? LastLoginDate { get => base.Record.LastLoginDate; set => base.Record.LastLoginDate = value; }
        public bool? Active { get => base.Record.Active; set => base.Record.Active = value; }
    }
}
