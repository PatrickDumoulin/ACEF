using BusinessLayer.Logic.Interfaces;
using CoreLib.Injection;
using DataAccess.BOL.Intervention;
using DataAccess.Models;
using DataModels.BOL.Client;
using DataModels.BOL.Intervention;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Core.Controllers;
using WebApp.ViewModels;
using System.Linq;

namespace WebApp.Controllers
{
    public class InterventionController : AbstractBLLController<IInterventionBLL>
    {
        private readonly IEmployeeBLL _employeeBLL;
        private readonly IClientBLL _clientBLL;

        public InterventionController() : base()
        {
            _employeeBLL = Injector.ImplementBll<IEmployeeBLL>();
            _clientBLL = Injector.ImplementBll<IClientBLL>();
        }

        // GET: InterventionController
        public IActionResult Index()
        {
            var response = bll.GetInterventions();
            if (response.Succeeded)
            {
                var viewModelList = response.ElementList.Select(e => MapToViewModel(e)).ToList();
                return View(viewModelList);
            }
            return NotFound();
        }

        // GET: InterventionController/Details/5
        public ActionResult Details(int id)
        {
            var response = bll.GetIntervention(id);
            if (response.Succeeded && response.Element != null)
            {
                var viewModel = MapToViewModel(response.Element);
                return View(viewModel);
            }
            return NotFound();
        }

        // GET: InterventionController/Create
        public ActionResult Create()
        {
            PopulateMdViewBags();
            ViewBag.Employees = GetEmployeesSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InterventionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newIntervention = MapToBOL(viewModel);
                bll.CreateIntervention(newIntervention);

                TempData["success"] = "Intervention créée avec succès";
                return RedirectToAction(nameof(Index));
            }

            PopulateMdViewBags();
            ViewBag.Employees = GetEmployeesSelectList();
            return View(viewModel);
        }

        // GET: InterventionController/Edit/5
        public ActionResult Edit(int id)
        {
            var response = bll.GetIntervention(id);
            if (response.Succeeded && response.Element != null)
            {
                var viewModel = MapToViewModel(response.Element);
                PopulateMdViewBags();
                ViewBag.Employees = GetEmployeesSelectList();
                return View(viewModel);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, InterventionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var intervention = MapToBOL(viewModel);
                bll.UpdateIntervention(intervention);

                TempData["success"] = "Intervention modifiée avec succès";
                return RedirectToAction(nameof(Index));
            }

            PopulateMdViewBags();
            ViewBag.Employees = GetEmployeesSelectList();
            return View(viewModel);
        }

        // GET: InterventionController/Delete/5
        public ActionResult Delete(int id)
        {
            var response = bll.GetIntervention(id);
            if (response.Succeeded && response.Element != null)
            {
                var viewModel = MapToViewModel(response.Element);
                return View(viewModel);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            bll.DeleteIntervention(id);

            TempData["success"] = "Intervention supprimée avec succès";
            return RedirectToAction(nameof(Index));
        }

        private InterventionViewModel MapToViewModel(IInterventionBOL intervention)
        {
            return new InterventionViewModel
            {
                Id = intervention.Id,
                IsVirtual = intervention.IsVirtual,
                DateIntervention = intervention.DateIntervention,
                IdEmployee = intervention.IdEmployee,
                IdClient = intervention.IdClient,
                IdReferenceType = intervention.IdReferenceType,
                IdStatusType = intervention.IdStatusType,
                IdInterventionType = intervention.IdInterventionType,
                DebtAmount = intervention.DebtAmount,
                IdLoanReason = intervention.IdLoanReason,
                IsLoanPaid = intervention.IsLoanPaid
            };
        }

        private InterventionBOL MapToBOL(InterventionViewModel viewModel)
        {
            return new InterventionBOL
            {
                IsVirtual = viewModel.IsVirtual,
                DateIntervention = viewModel.DateIntervention,
                IdEmployee = viewModel.IdEmployee,
                IdClient = viewModel.IdClient,
                IdReferenceType = viewModel.IdReferenceType,
                IdStatusType = viewModel.IdStatusType,
                IdInterventionType = viewModel.IdInterventionType,
                DebtAmount = viewModel.DebtAmount,
                IdLoanReason = viewModel.IdLoanReason,
                IsLoanPaid = viewModel.IsLoanPaid
            };
        }

        private void PopulateMdViewBags()
        {
            IMDBLL mdBLL = base.GetBLL<IMDBLL>();

            ViewBag.ReferenceTypes = new SelectList(mdBLL.GetAllMdReferenceSources().ElementList, "Id", "Name");
            ViewBag.StatusTypes = new SelectList(mdBLL.GetAllMdInterventionStatusTypes().ElementList, "Id", "Name");
            ViewBag.InterventionTypes = new SelectList(mdBLL.GetAllMdInterventionTypes().ElementList, "Id", "Name");
            ViewBag.LoanReasons = new SelectList(mdBLL.GetAllMdLoanReasons().ElementList, "Id", "Name");
            ViewBag.Solutions = new SelectList(mdBLL.GetAllMdInterventionSolutions().ElementList, "Id", "Name");
        }

        private SelectList GetEmployeesSelectList()
        {
            var response = _employeeBLL.GetAllEmployees();
            if (response.Succeeded)
            {
                var employees = response.ElementList.Select(e => new
                {
                    Id = e.Id.ToString(),
                    FullName = $"{e.FirstName} {e.LastName}"
                }).ToList();

                return new SelectList(employees, "Id", "FullName");
            }
            return new SelectList(Enumerable.Empty<SelectListItem>());
        }
        [HttpGet]
        [HttpGet]
        public JsonResult SearchClients(string searchTerm)
        {
            var clients = GetClients().Where(c =>
                c.FullName.ToLower().Contains(searchTerm.ToLower())).ToList();
            return Json(clients);
        }

        private IEnumerable<ClientViewModel> GetClients()
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
