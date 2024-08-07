using BusinessLayer.Logic.Interfaces;
using CoreLib.Injection;
using DataAccess.BOL.Intervention;
using DataAccess.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Core.Controllers;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class InterventionController : AbstractBLLController<IInterventionBLL>
    {
        private readonly IEmployeeBLL _employeeBLL;

        public InterventionController() : base()
        {
            _employeeBLL = Injector.ImplementBll<IEmployeeBLL>();
        }

        // GET: InterventionController
        public IActionResult Index()
        {
            var response = bll.GetInterventions();
            if (response.Succeeded)
            {
                var viewModelList = response.ElementList.Select(e => new InterventionViewModel
                {
                    Id = e.Id,
                    DateIntervention = e.DateIntervention,
                    IdEmployee = e.IdEmployee,
                    IdClient = e.IdClient,
                    IdReferenceType = e.IdReferenceType,
                    IdStatusType = e.IdStatusType,
                    IdLoanReason = e.IdLoanReason,
                    DebtAmount = e.DebtAmount,
                    IdInterventionType = e.IdInterventionType,
                    IsLoanPaid = e.IsLoanPaid
                }).ToList();

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
                var viewModel = new InterventionViewModel
                {
                    Id = response.Element.Id,
                    DateIntervention = response.Element.DateIntervention,
                    IdEmployee = response.Element.IdEmployee,
                    IdClient = response.Element.IdClient,
                    IdReferenceType = response.Element.IdReferenceType,
                    IdStatusType = response.Element.IdStatusType,
                    IdLoanReason = response.Element.IdLoanReason,
                    DebtAmount = response.Element.DebtAmount,
                    IdInterventionType = response.Element.IdInterventionType,
                    IsLoanPaid = response.Element.IsLoanPaid
                };
                return View(viewModel);
            }
            return NotFound();
        }

        // GET: InterventionController/Create
        public ActionResult Create()
        {
            IMDBLL mdBLL = base.GetBLL<IMDBLL>();
            PopulateMdViewBags(mdBLL);

            ViewBag.Employees = GetEmployeesSelectList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InterventionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var newIntervention = new InterventionBOL
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

                base.bll.CreateIntervention(newIntervention);
                TempData["success"] = "Intervention créee avec succès";
                return RedirectToAction(nameof(Index));
            }

            IMDBLL mdBLL = base.GetBLL<IMDBLL>();
            PopulateMdViewBags(mdBLL);
            ViewBag.Employees = GetEmployeesSelectList();

            return View(viewModel);
        }

        // GET: InterventionController/Edit/5
        public ActionResult Edit(int id)
        {
            var response = bll.GetIntervention(id);
            if (response.Succeeded && response.Element != null)
            {
                var viewModel = new InterventionViewModel
                {
                    Id = response.Element.Id,
                    DateIntervention = response.Element.DateIntervention,
                    IdEmployee = response.Element.IdEmployee,
                    IdClient = response.Element.IdClient,
                    IdReferenceType = response.Element.IdReferenceType,
                    IdStatusType = response.Element.IdStatusType,
                    IdLoanReason = response.Element.IdLoanReason,
                    DebtAmount = response.Element.DebtAmount,
                    IdInterventionType = response.Element.IdInterventionType,
                    IsLoanPaid = response.Element.IsLoanPaid
                };

                IMDBLL mdBLL = base.GetBLL<IMDBLL>();
                PopulateMdViewBags(mdBLL);
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
                var intervention = new InterventionBOL
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

                base.bll.UpdateIntervention(intervention);
                TempData["success"] = "Intervention modifiée avec succès";
                return RedirectToAction(nameof(Index));
            }

            IMDBLL mdBLL = base.GetBLL<IMDBLL>();
            PopulateMdViewBags(mdBLL);
            ViewBag.Employees = GetEmployeesSelectList();

            return View(viewModel);
        }

        // GET: InterventionController/Delete/5
        public ActionResult Delete(int id)
        {
            var response = bll.GetIntervention(id);
            if (response.Succeeded && response.Element != null)
            {
                var viewModel = new InterventionViewModel
                {
                    Id = response.Element.Id,
                    DateIntervention = response.Element.DateIntervention,
                    IdEmployee = response.Element.IdEmployee,
                    IdClient = response.Element.IdClient,
                    IdReferenceType = response.Element.IdReferenceType,
                    IdStatusType = response.Element.IdStatusType,
                    IdLoanReason = response.Element.IdLoanReason,
                    DebtAmount = response.Element.DebtAmount,
                    IdInterventionType = response.Element.IdInterventionType,
                    IsLoanPaid = response.Element.IsLoanPaid
                };
                return View(viewModel);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            bll.DeleteIntervention(id);
            return RedirectToAction(nameof(Index));
        }

        private void PopulateMdViewBags(IMDBLL mdBLL)
        {
            var mdReferenceSource = mdBLL.GetAllMdReferenceSources();
            var mdStatusType = mdBLL.GetAllMdInterventionStatusTypes();
            var mdLoanReason = mdBLL.GetAllMdLoanReasons();
            var mdInterventionType = mdBLL.GetAllMdInterventionTypes();

            ViewBag.MdReferenceSource = mdReferenceSource.ElementList.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            });

            ViewBag.MdInterventionStatusType = mdStatusType.ElementList.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            });

            ViewBag.MdLoanReason = mdLoanReason.ElementList.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            });

            ViewBag.MdInterventionType = mdInterventionType.ElementList.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            });
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
    }
}
