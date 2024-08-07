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
    public class EmployeeBLL : AbstractBLL<IEmployeeDAL>, IEmployeeBLL
    {
        public EmployeeBLL() { }
        public EmployeeBLL(ProviderDALTypes dalType) : base(dalType) { }
        public EmployeeBLL(IDAL externalDAL) : base(externalDAL) { }

        public GetListResponse<Employees> GetAllEmployees()
        {
            var employees = base.dal.GetAllEmployees();
            return new GetListResponse<Employees>(employees.ToList());
        }

        public GetItemResponse<Employees> GetEmployeeById(int id)
        {
            var employee = base.dal.GetEmployeeById(id);
            return new GetItemResponse<Employees>(employee);
        }

        public void CreateEmployee(Employees employee)
        {
            base.dal.CreateEmployee(employee);
        }

        public void UpdateEmployee(Employees employee)
        {
            base.dal.UpdateEmployee(employee);
        }

        public void DeleteEmployee(int id)
        {
            base.dal.DeleteEmployee(id);
        }
    }
}
