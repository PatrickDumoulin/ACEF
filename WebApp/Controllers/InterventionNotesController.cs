using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.BOL.InterventionNote;
using DataAccess.BOL.Note;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core.Controllers;
using WebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using CoreLib.Injection;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Superutilisateur, Intervenant")]
    public class InterventionNotesController : AbstractBLLController<IInterventionNoteBLL>
    {
        

        private readonly IEmployeeBLL _employeeBLL;

        private readonly UserManager<IdentityUser> _userManager;

        public InterventionNotesController() : base()
        {
            _employeeBLL = Injector.ImplementBll<IEmployeeBLL>();
            
        }





        public IActionResult Index(int interventionId)
        {
            ViewBag.Title = "Ajouter une note";
            var interventionResponse = GetBLL<IInterventionBLL>().GetIntervention(interventionId);
            if (!interventionResponse.Succeeded || interventionResponse.Element == null)
            {
                return NotFound();
            }

            var notesResponse = bll.GetInterventionNotesByInterventionId(interventionId);

            var currentUserName = HttpContext.User.Identity.Name;

            var viewModel = new InterventionNotesViewModel(_employeeBLL)
            {
                Intervention = new InterventionViewModel
                {
                    Id = interventionResponse.Element.Id,
                    EmployeeName = currentUserName
                },
                InterventionNotes = notesResponse.ElementList,
                CreatedBy = currentUserName
            };

            return View(viewModel);
        }

        public IActionResult Create(int interventionId)
        {
            // Récupérer l'utilisateur actuellement connecté
            var currentUserName = HttpContext.User.Identity.Name;
            var employee = _employeeBLL.GetEmployeeByUsername(currentUserName).Element;

            if (employee == null)
            {
                ModelState.AddModelError("", "Impossible de trouver l'enregistrement de l'employé.");
                return View();
            }

            var note = new InterventionNoteBOL
            {
                IdIntervention = interventionId,
                IdEmployee = employee.Id  // Assigner l'ID de l'employé à la note
            };

            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InterventionNoteBOL note)
        {
            // Récupérer l'utilisateur actuellement connecté
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
                bll.CreateInterventionNote(note);

                TempData["success"] = "Note créée avec succès";
                return RedirectToAction(nameof(Index), new { interventionId = note.IdIntervention });
            }

            return View(note);
        }

        public IActionResult Edit(int id)
        {
            
            var response = bll.GetInterventionNoteById(id);
            if (response.Element == null)
            {
                return NotFound();
            }

            return View(response.Element);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, InterventionNoteBOL note)
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
                bll.UpdateInterventionNote(note);
                TempData["success"] = "Note modifiée avec succès";
                return RedirectToAction(nameof(Index), new { interventionId = note.IdIntervention });
            }

            return View(note);
        }

        public IActionResult Delete(int id)
        {
            var response = bll.GetInterventionNoteById(id);
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
            var note = bll.GetInterventionNoteById(id).Element;
            bll.DeleteInterventionNote(id);
            
            return RedirectToAction(nameof(Index), new { interventionId = note.IdIntervention });
        }
    }
}
