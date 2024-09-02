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
using System.Web.Mvc;

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

        public SelectList GetEmployeesSelectList()
        {
            var response = GetAllEmployees();
            if (response.Succeeded)
            {
                var employees = response.ElementList.Select(e => new
                {
                    Id = e.Id.ToString(),
                    FullName = $"{e.FirstName} {e.LastName}"
                }).ToList();

                return new SelectList(employees, "Id", "FullName");
            }
            return new SelectList(Enumerable.Empty<SelectListItem>());
        }

        public string GetEmployeeName(int employeeId)
        {
            var employeeResponse = GetEmployeeById(employeeId);
            return employeeResponse.Succeeded && employeeResponse.Element != null
                ? $"{employeeResponse.Element.FirstName} {employeeResponse.Element.LastName}"
                : "Inconnu";
        }

        public GetItemResponse<Employees> GetEmployeeByUsername(string userName)
        {
            // Rechercher l'employé par son nom d'utilisateur dans la DAL
            var employee = base.dal.GetEmployeeByUsername(userName);

            // Retourner une réponse contenant l'employé trouvé
            return new GetItemResponse<Employees>(employee);
        }


    }
}
