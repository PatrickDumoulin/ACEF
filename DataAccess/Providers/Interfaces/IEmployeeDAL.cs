using CoreLib.Definitions;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Providers.Interfaces
{
    public interface IEmployeeDAL : IDAL
    {
        IEnumerable<Employees> GetAllEmployees();
        Employees GetEmployeeById(int id);
        void CreateEmployee(Employees employee);
        void UpdateEmployee(Employees employee);
        void DeleteEmployee(int id);

        public Employees GetEmployeeByUsername(string userName);
    }
}
