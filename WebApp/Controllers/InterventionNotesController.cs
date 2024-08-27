using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using DataAccess.BOL.InterventionNote;
using DataAccess.BOL.Note;
using Microsoft.AspNetCore.Mvc;
using WebApp.Core.Controllers;
using WebApp.ViewModels;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace WebApp.Controllers
{
    public class InterventionNotesController : AbstractBLLController<IInterventionNoteBLL>
    {
        

        private readonly IEmployeeBLL _employeeBLL;

        private readonly UserManager<IdentityUser> _userManager;

        public InterventionNotesController() : base() { }

        

        public IActionResult Index(int interventionId)
        {
            var interventionResponse = GetBLL<IInterventionBLL>().GetIntervention(interventionId);
            if (!interventionResponse.Succeeded || interventionResponse.Element == null)
            {
                return NotFound();
            }

            var notesResponse = bll.GetInterventionNotesByInterventionId(interventionId);

            var currentUserName = HttpContext.User.Identity.Name;

            var viewModel = new InterventionNotesViewModel
            {
                Intervention = new InterventionViewModel
                {
                    Id = interventionResponse.Element.Id,
                    EmployeeName = currentUserName
                },
                InterventionNotes = notesResponse.ElementList
            };

            return View(viewModel);
        }

        public IActionResult Create(int interventionId)
        {
            var note = new InterventionNoteBOL { IdIntervention = interventionId };
            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InterventionNoteBOL note)
        {
            if (ModelState.IsValid)
            {
                bll.CreateInterventionNote(note);
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
            if (id != note.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                bll.UpdateInterventionNote(note);
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var note = bll.GetInterventionNoteById(id).Element;
            bll.DeleteInterventionNote(id);
            return RedirectToAction(nameof(Index), new { interventionId = note.IdIntervention });
        }
    }
}
