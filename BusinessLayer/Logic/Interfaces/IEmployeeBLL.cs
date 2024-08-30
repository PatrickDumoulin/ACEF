using BusinessLayer.Communication.Responses.Common;
using CoreLib.Definitions;
using DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace BusinessLayer.Logic.Interfaces
{
    public interface IEmployeeBLL: IBLL
    {
        GetListResponse<Employees> GetAllEmployees();
        GetItemResponse<Employees> GetEmployeeById(int id);
        void CreateEmployee(Employees employee);
        void UpdateEmployee(Employees employee);
        void DeleteEmployee(int id);

        SelectList GetEmployeesSelectList();

        string GetEmployeeName(int employeeId);

        public GetItemResponse<Employees> GetEmployeeByUsername(string userName);
    }
}
