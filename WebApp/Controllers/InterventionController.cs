using BusinessLayer.Logic.Interfaces;
using CoreLib.Injection;
using DataAccess.Models;
using DataModels.BOL.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using WebApp.Core.Controllers;
using WebApp.ViewModels;
using System.Linq;
using DataModels.BOL.Intervention;
using DataAccess.BOL.InterventionsInterventionSolutions;
using Microsoft.EntityFrameworkCore;
using DataModels.BOL.InterventionsInterventionSolutions;
using BusinessLayer.Logic;
using System;
using System.Collections.Generic;

namespace WebApp.Controllers
{
    public class InterventionController : AbstractBLLController<IInterventionBLL>
    {
        private readonly IEmployeeBLL _employeeBLL;
        private readonly IClientBLL _clientBLL;
        private readonly IInterventionBLL _interventionBLL;
        private readonly IInterventionsInterventionSolutionsBLL _solutionsBLL;
        private readonly IMDBLL _mdbBLL;

        public InterventionController() : base()
        {
            _employeeBLL = Injector.ImplementBll<IEmployeeBLL>();
            _clientBLL = Injector.ImplementBll<IClientBLL>();
            _interventionBLL = Injector.ImplementBll<IInterventionBLL>();
            _solutionsBLL = Injector.ImplementBll<IInterventionsInterventionSolutionsBLL>();
            _mdbBLL = Injector.ImplementBll<IMDBLL>();
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
            var model = new InterventionViewModel
            {
                Solutions = new SelectList(ViewBag.Solutions, "Value", "Text"),
                SelectedSolutions = new List<int>()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InterventionViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
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
                        DebtAmount = viewModel.DebtAmount.HasValue ? EncryptDebtAmount(viewModel.DebtAmount.Value) : null,
                        IdLoanReason = viewModel.IdLoanReason,
                        IsLoanPaid = viewModel.IsLoanPaid,
                    };

                    _interventionBLL.CreateIntervention(newIntervention);

                    if (newIntervention.Id > 0 && viewModel.SelectedSolutions != null && viewModel.SelectedSolutions.Any())
                    {
                        foreach (var solutionId in viewModel.SelectedSolutions)
                        {
                            var solutionBOL = new InterventionsInterventionSolutionsBOL
                            {
                                IdIntervention = newIntervention.Id,
                                IdInterventionSolution = solutionId
                            };
                            _solutionsBLL.CreateInterventionsInterventionSolutions(solutionBOL);
                        }
                    }

                    TempData["success"] = "Intervention créée avec succès";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException dbEx)
                {
                    ModelState.AddModelError(string.Empty, $"Erreur lors de la mise à jour de la base de données : {dbEx.Message}");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Erreur inattendue : {ex.Message}");
                }
            }

            PopulateMdViewBags();
            ViewBag.Employees = GetEmployeesSelectList();
            viewModel.Solutions = ViewBag.Solutions ?? new SelectList(Enumerable.Empty<SelectListItem>());
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
                viewModel.Solutions = new SelectList(ViewBag.Solutions, "Value", "Text");
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

                // Mettre à jour les solutions d'intervention
                if (viewModel.SelectedSolutions != null)
                {
                    // Suppression des anciennes solutions
                    intervention.InterventionsInterventionSolutions.Clear();
                    foreach (var solutionId in viewModel.SelectedSolutions)
                    {
                        intervention.InterventionsInterventionSolutions.Add(new InterventionsInterventionSolutionsBOL
                        {
                            IdInterventionSolution = solutionId
                        });
                    }
                }

                bll.UpdateIntervention(intervention);
                TempData["success"] = "Intervention modifiée avec succès";
                return RedirectToAction(nameof(Index));
            }

            PopulateMdViewBags();
            ViewBag.Employees = GetEmployeesSelectList();
            viewModel.Solutions = new SelectList(ViewBag.Solutions, "Value", "Text");
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

        [HttpPost, ActionName("DeleteConfirmed")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                var allSolutions = _solutionsBLL.GetInterventionsInterventionSolutions();

                if (allSolutions.Succeeded && allSolutions.ElementList != null)
                {
                    var solutions = allSolutions.ElementList.Where(s => s.IdIntervention == id).ToList();

                    foreach (var solution in solutions)
                    {
                        _solutionsBLL.DeleteInterventionsInterventionSolutions(solution.Id);
                    }
                }

                bll.DeleteIntervention(id);

                TempData["success"] = "Intervention supprimée avec succès";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                TempData["error"] = $"Erreur lors de la suppression : {ex.Message}";
                return RedirectToAction(nameof(Index));
            }
        }

        private InterventionViewModel MapToViewModel(IInterventionBOL intervention)
        {
            var employeeName = intervention.IdEmployee.HasValue ? GetEmployeeName(intervention.IdEmployee.Value) : "Inconnu";
            var clientName = intervention.IdClient.HasValue ? GetClientName(intervention.IdClient.Value) : "Inconnu";
            var statusName = intervention.IdStatusType.HasValue ? GetStatusName(intervention.IdStatusType.Value) : "Inconnu";
            var referenceTypeName = intervention.IdReferenceType.HasValue ? GetReferenceTypeName(intervention.IdReferenceType.Value) : "Inconnu";
            var interventionTypeName = intervention.IdInterventionType.HasValue ? GetInterventionTypeName(intervention.IdInterventionType.Value) : "Inconnu";
            var solutionNames = intervention.InterventionSolutionsIds.Select(GetSolutionName).ToList();

            return new InterventionViewModel
            {
                Id = intervention.Id,
                IsVirtual = intervention.IsVirtual,
                DateIntervention = intervention.DateIntervention,
                EmployeeName = employeeName,
                IdEmployee = intervention.IdEmployee,
                IdClient = intervention.IdClient,
                ClientName = clientName,
                IdReferenceType = intervention.IdReferenceType,
                ReferenceTypeName = referenceTypeName,
                IdStatusType = intervention.IdStatusType,
                StatusName = statusName,
                IdInterventionType = intervention.IdInterventionType,
                InterventionTypeName = interventionTypeName,
                DebtAmount = intervention.DebtAmount != null ? DecryptDebtAmount(intervention.DebtAmount) : (decimal?)null,
                IdLoanReason = intervention.IdLoanReason,
                IsLoanPaid = intervention.IsLoanPaid,
                SelectedSolutions = intervention.InterventionSolutionsIds.ToList(),
                Solutions = new SelectList(ViewBag.Solutions ?? Enumerable.Empty<SelectListItem>(), "Value", "Text"),
                SolutionNames = solutionNames
            };
        }

        private InterventionBOL MapToBOL(InterventionViewModel viewModel)
        {
            var interventionBOL = new InterventionBOL
            {
                IsVirtual = viewModel.IsVirtual,
                DateIntervention = viewModel.DateIntervention,
                IdEmployee = viewModel.IdEmployee,
                IdClient = viewModel.IdClient,
                IdReferenceType = viewModel.IdReferenceType,
                IdStatusType = viewModel.IdStatusType,
                IdInterventionType = viewModel.IdInterventionType,
                DebtAmount = viewModel.DebtAmount.HasValue ? EncryptDebtAmount(viewModel.DebtAmount.Value) : null,
                IdLoanReason = viewModel.IdLoanReason,
                IsLoanPaid = viewModel.IsLoanPaid
            };

            if (viewModel.SelectedSolutions != null)
            {
                foreach (var solutionId in viewModel.SelectedSolutions)
                {
                    interventionBOL.InterventionsInterventionSolutions.Add(new InterventionsInterventionSolutionsBOL
                    {
                        IdInterventionSolution = solutionId
                    });
                }
            }

            return interventionBOL;
        }

        private void PopulateMdViewBags()
        {
            ViewBag.ReferenceTypes = new SelectList(_mdbBLL.GetAllMdReferenceSources().ElementList, "Id", "Name");
            ViewBag.StatusTypes = new SelectList(_mdbBLL.GetAllMdInterventionStatusTypes().ElementList, "Id", "Name");
            ViewBag.InterventionTypes = new SelectList(_mdbBLL.GetAllMdInterventionTypes().ElementList, "Id", "Name");
            ViewBag.LoanReasons = new SelectList(_mdbBLL.GetAllMdLoanReasons().ElementList, "Id", "Name");
            ViewBag.Solutions = new SelectList(_mdbBLL.GetAllMdInterventionSolutions().ElementList, "Id", "Name");
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

        // Méthodes de chiffrement et déchiffrement
        private byte[] EncryptDebtAmount(decimal amount)
        {
            return BitConverter.GetBytes((double)amount);
        }

        private decimal DecryptDebtAmount(byte[] encryptedAmount)
        {
            return (decimal)BitConverter.ToDouble(encryptedAmount, 0);
        }

        private string GetEmployeeName(int employeeId)
        {
            var employeeResponse = _employeeBLL.GetEmployeeById(employeeId);
            return employeeResponse.Succeeded && employeeResponse.Element != null
                ? $"{employeeResponse.Element.FirstName} {employeeResponse.Element.LastName}"
                : "Inconnu";
        }

        private string GetClientName(int clientId)
        {
            var clientResponse = _clientBLL.GetClient(clientId);
            return clientResponse.Succeeded && clientResponse.Element != null
                ? $"{clientResponse.Element.FirstName} {clientResponse.Element.LastName}"
                : "Inconnu";
        }

        private string GetStatusName(int statusId)
        {
            var statusResponse = _mdbBLL.GetMdInterventionStatusType(statusId);
            return statusResponse.Succeeded && statusResponse.Element != null
                ? statusResponse.Element.Name
                : "Inconnu";
        }

        private string GetReferenceTypeName(int referenceTypeId)
        {
            var referenceResponse = _mdbBLL.GetMdReferenceSource(referenceTypeId);
            return referenceResponse.Succeeded && referenceResponse.Element != null
                ? referenceResponse.Element.Name
                : "Inconnu";
        }

        private string GetInterventionTypeName(int interventionTypeId)
        {
            var interventionResponse = _mdbBLL.GetMdInterventionType(interventionTypeId);
            return interventionResponse.Succeeded && interventionResponse.Element != null
                ? interventionResponse.Element.Name
                : "Inconnu";
        }

        private string GetSolutionName(int solutionId)
        {
            var solutionResponse = _mdbBLL.GetMdInterventionSolution(solutionId);
            return solutionResponse.Succeeded && solutionResponse.Element != null
                ? solutionResponse.Element.Name
                : "Inconnu";
        }
    }
}
