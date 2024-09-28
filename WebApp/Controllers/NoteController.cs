using BusinessLayer.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DataAccess.BOL.Note;
using System.Linq;
using WebApp.ViewModels;
using CoreLib.Definitions;
using WebApp.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using BusinessLayer.Logic;
using CoreLib.Injection;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Superutilisateur, Intervenant")]
    public class NoteController : AbstractBLLController<INoteBLL>
    {
        private readonly IEmployeeBLL _employeeBLL;

        private readonly UserManager<IdentityUser> _userManager;

        public NoteController() : base()
        {
            _employeeBLL = Injector.ImplementBll<IEmployeeBLL>();

        }

        //public NoteController() : base() { }

        public IActionResult Index(int clientId)
        {
            var clientResponse = GetBLL<IClientBLL>().GetClient(clientId);
            if (!clientResponse.Succeeded || clientResponse.Element == null)
            {
                return NotFound();
            }

            var notesResponse = bll.GetNotesByClientId(clientId);

            var viewModel = new ClientNotesViewModel(_employeeBLL)
            {
                Client = new ClientViewModel
                {
                    Id = clientResponse.Element.Id,
                    FirstName = clientResponse.Element.FirstName,
                    LastName = clientResponse.Element.LastName
                },
                Notes = notesResponse.ElementList
            };

            return View(viewModel);
        }

        public IActionResult Create(int clientId)
        {
            var currentUserName = HttpContext.User.Identity.Name;
            var employee = _employeeBLL.GetEmployeeByUsername(currentUserName).Element;

            if (employee == null)
            {
                ModelState.AddModelError("", "Impossible de trouver l'enregistrement de l'employé.");
                return View();
            }

            var note = new NoteBOL 
            { 
                IdClient = clientId,
                IdEmployee = employee.Id  // Assigner l'ID de l'employé à la note
            };
            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NoteBOL note)
        {
            var currentUserName = HttpContext.User.Identity.Name;
            var employee = _employeeBLL.GetEmployeeByUsername(currentUserName).Element;
            
            if (employee == null)
            {
                ModelState.AddModelError("", "Unable to find the employee record.");
                return View(note);
            }

            // Assigner l'ID de l'employé à la note
            note.IdEmployee = employee.Id;

            if (ModelState.IsValid)
            {
                bll.CreateNote(note);
                TempData["success"] = "Note créée avec succès";
                return RedirectToAction(nameof(Index), new { clientId = note.IdClient });
            }

            return View(note);
        }

        public IActionResult Edit(int id)
        {
            var response = bll.GetNoteById(id);
            if (response.Element == null)
            {
                return NotFound();
            }

            return View(response.Element);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, NoteBOL note)
        {
            var currentUserName = HttpContext.User.Identity.Name;
            var employee = _employeeBLL.GetEmployeeByUsername(currentUserName).Element;
            
            if (employee == null)
            {
                ModelState.AddModelError("", "Unable to find the employee record.");
                return View(note);
            }

            // Assigner l'ID de l'employé à la note
            note.IdEmployee = employee.Id;

            if (id != note.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                bll.UpdateNote(note);
                TempData["success"] = "Note modifiée avec succès";
                return RedirectToAction(nameof(Index), new { clientId = note.IdClient });
            }

            return View(note);
        }

        public IActionResult Delete(int id)
        {
            var response = bll.GetNoteById(id);
            if (response.Element == null)
            {
                return NotFound();
            }

            return View(response.Element);
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var note = bll.GetNoteById(id).Element;
            bll.DeleteNote(id);
            return RedirectToAction(nameof(Index), new { clientId = note.IdClient });
        }
    }
}
