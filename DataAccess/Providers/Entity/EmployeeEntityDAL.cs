using DataAccess.Core.Definitions;
using DataAccess.Models;
using DataAccess.Providers.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace DataAccess.Providers.Entity
{
    public class EmployeeEntityDAL : AcefEntityDAL, IEmployeeDAL
    {
        public EmployeeEntityDAL() { }
        public EmployeeEntityDAL(AcefEntityDAL externalDal) : base(externalDal) { }

        public IEnumerable<Employees> GetAllEmployees()
        {
            return Db.Employees.ToList();
        }

        public Employees GetEmployeeById(int id)
        {
            return Db.Employees.AsNoTracking().FirstOrDefault(e => e.Id == id);
        }

        public void CreateEmployee(Employees employee)
        {
            Db.Employees.Add(employee);
            Db.SaveChanges();
        }

        public void UpdateEmployee(Employees employee)
        {
            Db.Employees.Update(employee);
            Db.SaveChanges();
        }

        public void DeleteEmployee(int id)
        {
            var employee = Db.Employees.Find(id);
            if (employee != null)
            {
                Db.Employees.Remove(employee);
                Db.SaveChanges();
            }
        }
    }
}
