using BusinessLayer.Logic.Interfaces;
using DataAccess.BOL.InterventionNote;
using DataAccess.BOL.Note;

namespace WebApp.ViewModels
{
    public class InterventionNotesViewModel
    {
        private readonly IEmployeeBLL _employeeBLL;

        public InterventionNotesViewModel(IEmployeeBLL employeeBLL)
        {
            _employeeBLL = employeeBLL;
        }

        public InterventionViewModel Intervention { get; set; }
        public IEnumerable<InterventionNoteBOL> InterventionNotes { get; set; }
        public string CreatedBy { get; set; }

        public string GetEmployeeUsernameByEmployeeId(int? id)
        {
            if (id == null)
            {
                return "Inconnu";
            }
            var employee = _employeeBLL.GetEmployeeById(id.Value).Element;
            return employee != null ? employee.UserName : "Inconnu";
        }

        public string EmployeeName { get; set; }
    }
}
