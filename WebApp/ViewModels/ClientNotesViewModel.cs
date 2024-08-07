using DataAccess.BOL.Note;
using System.Collections.Generic;

namespace WebApp.ViewModels
{
    public class ClientNotesViewModel
    {
        public ClientViewModel Client { get; set; }
        public IEnumerable<NoteBOL> Notes { get; set; }
    }
}
