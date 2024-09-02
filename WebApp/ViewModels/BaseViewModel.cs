using BusinessLayer.Logic.Interfaces;

namespace WebApp.ViewModels
{
    public abstract class BaseViewModel
    {
        private readonly IEmployeeBLL _employeeBLL;

        protected BaseViewModel(IEmployeeBLL employeeBLL)
        {
            _employeeBLL = employeeBLL;
        }

        public string GetEmployeeUsernameByEmployeeId(int? id)
        {
            if (id == null)
            {
                return "Inconnu";
            }
            var employee = _employeeBLL.GetEmployeeById(id.Value).Element;
            return employee != null ? employee.UserName : "Inconnu";
        }
    }
}
