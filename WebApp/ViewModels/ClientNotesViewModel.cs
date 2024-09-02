using BusinessLayer.Logic.Interfaces;
using DataAccess.BOL.Note;
using System.Collections.Generic;

namespace WebApp.ViewModels
{
    public class ClientNotesViewModel: BaseViewModel
    {


        public ClientNotesViewModel(IEmployeeBLL employeeBLL) : base(employeeBLL)
        {
        }
        public ClientViewModel Client { get; set; }
        public IEnumerable<NoteBOL> Notes { get; set; }

        
    }
}
