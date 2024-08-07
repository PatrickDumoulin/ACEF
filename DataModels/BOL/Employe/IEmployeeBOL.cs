using CoreLib.Definitions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataModels.BOL.Employe
{
    public interface IEmployeeBOL : IBOL
    {
        int Id { get; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string UserName { get; set; }
        string PasswordHash { get; set; }
        DateTime? LastLoginDate { get; set; }
        bool? Active { get; set; }
    }
}
