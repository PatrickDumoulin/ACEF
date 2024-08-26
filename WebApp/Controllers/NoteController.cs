using BusinessLayer.Logic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using DataAccess.BOL.Note;
using System.Linq;
using WebApp.ViewModels;
using CoreLib.Definitions;
using WebApp.Core.Controllers;
using Microsoft.AspNetCore.Authorization;

namespace WebApp.Controllers
{
    
    public class NoteController : AbstractBLLController<INoteBLL>
    {
        public NoteController() : base() { }

        public IActionResult Index(int clientId)
        {
            var clientResponse = GetBLL<IClientBLL>().GetClient(clientId);
            if (!clientResponse.Succeeded || clientResponse.Element == null)
            {
                return NotFound();
            }

            var notesResponse = bll.GetNotesByClientId(clientId);

            var viewModel = new ClientNotesViewModel
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
            var note = new NoteBOL { IdClient = clientId };
            return View(note);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(NoteBOL note)
        {
            if (ModelState.IsValid)
            {
                bll.CreateNote(note);
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
            if (id != note.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                bll.UpdateNote(note);
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

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var note = bll.GetNoteById(id).Element;
            bll.DeleteNote(id);
            return RedirectToAction(nameof(Index), new { clientId = note.IdClient });
        }
    }
}
