using Microsoft.AspNetCore.Mvc;
using BusinessLayer.Logic.Interfaces;
using WebApp.ViewModels;
using WebApp.Core.Controllers;
using System;
using DataAccess.BOL.Seminar;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Models;
using CoreLib.Injection;

namespace WebApp.Controllers
{
    public class SeminarsController : AbstractBLLController<ISeminarBLL>
    {
        private readonly IClientBLL _clientBLL;

        public SeminarsController() : base()
        {
            _clientBLL = Injector.ImplementBll<IClientBLL>();
        }

        public IActionResult Index()
        {
            var response = bll.GetAllSeminars();
            if (response.Succeeded)
            {
                return View(response.ElementList);
            }
            return NotFound();
        }

        public IActionResult Details(int id)
        {
            var response = bll.GetSeminarById(id);
            if (response.Succeeded && response.Element != null)
            {
                var seminar = response.Element;
                seminar.Participants = GetParticipants().Select(c => new Clients { Id = c.Id, FirstName = c.FirstName, LastName = c.LastName }).ToList();

                return View(seminar);
            }
            return NotFound();
        }

        public IActionResult Create()
        {
            var themes = GetSeminarThemes();
            ViewBag.SeminarThemes = new SelectList(themes, "Id", "Name");
            ViewBag.Participants = new SelectList(GetParticipants(), "Id", "FullName");

            var model = new SeminarViewModel
            {
                SelectedParticipants = new List<int>()
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(SeminarViewModel model)
        {
            if (ModelState.IsValid)
            {
                var seminar = new SeminarBOL
                (
                    model.DateSeminar,
                    model.IdSeminarTheme,
                    model.Notes,
                    DateTime.Now
                );

                seminar.Participants = model.SelectedParticipants.Select(id => new Clients { Id = id }).ToList();

                bll.CreateSeminar(seminar);
                return RedirectToAction(nameof(Index));
            }

            var themes = GetSeminarThemes();
            ViewBag.SeminarThemes = new SelectList(themes, "Id", "Name");
            ViewBag.Participants = new SelectList(GetParticipants(), "Id", "FullName");
            return View(model);
        }

        public IActionResult Edit(int id)
        {
            var response = bll.GetSeminarById(id);
            if (response.Succeeded && response.Element != null)
            {
                var model = new SeminarViewModel
                {
                    Id = response.Element.Id,
                    DateSeminar = response.Element.DateSeminar ?? DateTime.MinValue,
                    IdSeminarTheme = response.Element.IdSeminarTheme ?? 0,
                    Notes = response.Element.Notes,
                    SelectedParticipants = response.Element.Participants.Select(p => p.Id).ToList(),
                };

                var themes = GetSeminarThemes();
                ViewBag.SeminarThemes = new SelectList(themes, "Id", "Name");
                ViewBag.Participants = new SelectList(GetParticipants(), "Id", "FullName");
                return View(model);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, SeminarViewModel model)
        {
            if (ModelState.IsValid)
            {
                var seminar = new SeminarBOL
                (
                    model.DateSeminar,
                    model.IdSeminarTheme,
                    model.Notes,
                    DateTime.Now
                )
                {
                    Participants = model.SelectedParticipants.Select(p => new Clients { Id = p }).ToList(),
                };

                bll.UpdateSeminar(seminar);
                return RedirectToAction(nameof(Index));
            }

            var themes = GetSeminarThemes();
            ViewBag.SeminarThemes = new SelectList(themes, "Id", "Name");
            ViewBag.Participants = new SelectList(GetParticipants(), "Id", "FullName");
            return View(model);
        }

        public IActionResult Delete(int id)
        {
            var response = bll.GetSeminarById(id);
            if (response.Succeeded && response.Element != null)
            {
                return View(response.Element);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            bll.DeleteSeminar(id);
            return RedirectToAction(nameof(Index));
        }

        private IEnumerable<MdSeminarThemes> GetSeminarThemes()
        {
            var response = bll.GetSeminarThemes();
            if (response.Succeeded)
            {
                return response.ElementList;
            }
            return new List<MdSeminarThemes>();
        }

        [HttpGet]
        public JsonResult SearchParticipants(string searchTerm)
        {
            var participants = GetParticipants().Where(p =>
                p.FullName.ToLower().Contains(searchTerm.ToLower())).ToList();
            return Json(participants);
        }

        private IEnumerable<ClientViewModel> GetParticipants()
        {
            var response = _clientBLL.GetClients();
            if (response.Succeeded)
            {
                return response.ElementList.Select(c => new ClientViewModel
                {
                    Id = c.Id,
                    FullName = $"{c.FirstName} {c.LastName}"
                });
            }
            return new List<ClientViewModel>();
        }
    }
}
