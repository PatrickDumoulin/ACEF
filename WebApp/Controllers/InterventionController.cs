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
using DataAccess.BOL.Client;
using System.Net.Sockets;
using DataAccess.BOL.MdInterventionSolution;

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


        // GET: ClientController
        public IActionResult Index(InterventionSearchViewModel searchModel, string sortOrder, int page = 1, int pageSize = 6)
        {
            // Récupérez les interventions de la BLL
            var interventionsBOL = base.bll.GetInterventions().ElementList;

            // Convertir les BOL en ViewModel
            var interventions = interventionsBOL.Select(bol => MapToViewModel(bol)).ToList();

            // Filtrer par date
            DateTime now = DateTime.Now;
            if (searchModel.DateFilter == "Semaine en cours")
            {
                DateTime startOfWeek = _interventionBLL.StartOfWeek(now, DayOfWeek.Monday);
                DateTime endOfWeek = startOfWeek.AddDays(6);
                interventions = interventions.Where(i => i.DateIntervention >= startOfWeek && i.DateIntervention <= endOfWeek).ToList();
            }
            else if (searchModel.DateFilter == "Mois en cours")
            {
                DateTime startOfMonth = new DateTime(now.Year, now.Month, 1);
                DateTime endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);
                interventions = interventions.Where(i => i.DateIntervention >= startOfMonth && i.DateIntervention <= endOfMonth).ToList();
            }
            else if (searchModel.DateFilter == "Intervalle" && searchModel.StartDate.HasValue && searchModel.EndDate.HasValue)
            {
                interventions = interventions.Where(i => i.DateIntervention >= searchModel.StartDate && i.DateIntervention <= searchModel.EndDate).ToList();
            }

            // Populate the Employees and InterventionTypes lists
            var employees = _employeeBLL.GetAllEmployees().ElementList.Select(e => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = $"{e.FirstName} {e.LastName}"
            }).ToList();

            var interventionTypes = _mdbBLL.GetAllMdInterventionTypes().ElementList.Select(it => new SelectListItem
            {
                Value = it.Id.ToString(),
                Text = it.Name
            }).ToList();

            // Appliquer les filtres de recherche sur les ViewModel
            if (searchModel.Id.HasValue)
            {
                interventions = interventions.Where(c => c.Id == searchModel.Id.Value).ToList();
            }
            if (searchModel.IdClient.HasValue)
            {
                interventions = interventions.Where(c => c.IdClient == searchModel.IdClient.Value).ToList();
            }
            if (searchModel.IdEmployee.HasValue)
            {
                interventions = interventions.Where(c => c.IdEmployee == searchModel.IdEmployee.Value).ToList();
            }
            if (searchModel.IdInterventionType.HasValue)
            {
                interventions = interventions.Where(c => c.IdInterventionType == searchModel.IdInterventionType.Value).ToList();
            }
            if (searchModel.IsLoanUnpaid.HasValue)
            {
                interventions = interventions.Where(c => c.IsLoanPaid == !searchModel.IsLoanUnpaid.Value).ToList();
            }

            // Apply sorting
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.ClientIdSortParm = sortOrder == "IdClient" ? "clientId_desc" : "IdClient";
            ViewBag.EmployeeIdSortParm = sortOrder == "IdEmployee" ? "employeeId_desc" : "IdEmployee";
            ViewBag.InterventionTypeSortParm = sortOrder == "IdInterventionType" ? "interventionType_desc" : "IdInterventionType";

            switch (sortOrder)
            {
                case "id_desc":
                    interventions = interventions.OrderByDescending(c => c.Id).ToList();
                    break;
                case "IdClient":
                    interventions = interventions.OrderBy(c => c.IdClient).ToList();
                    break;
                case "clientId_desc":
                    interventions = interventions.OrderByDescending(c => c.IdClient).ToList();
                    break;
                case "IdEmployee":
                    interventions = interventions.OrderBy(c => c.IdEmployee).ToList();
                    break;
                case "employeeId_desc":
                    interventions = interventions.OrderByDescending(c => c.IdEmployee).ToList();
                    break;
                case "IdInterventionType":
                    interventions = interventions.OrderBy(c => c.IdInterventionType).ToList();
                    break;
                case "interventionType_desc":
                    interventions = interventions.OrderByDescending(c => c.IdInterventionType).ToList();
                    break;
                default:
                    interventions = interventions.OrderBy(c => c.Id).ToList();
                    break;
            }

            var totalcount = interventions.Count;
            var totalPages = (int)Math.Ceiling((decimal)totalcount / pageSize);
            var pagedInterventions = interventions.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new InterventionSearchViewModel
            {
                Id = searchModel.Id,
                IdClient = searchModel.IdClient,
                IdEmployee = searchModel.IdEmployee,
                IdInterventionType = searchModel.IdInterventionType,
                IsLoanUnpaid = searchModel.IsLoanUnpaid,
                Employees = employees,
                InterventionTypes = interventionTypes,
                Interventions = pagedInterventions // Assign the filtered, sorted, and paginated interventions
            };

            return View(viewModel);
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
            IMDBLL mdBLL = base.GetBLL<IMDBLL>();
            PopulateMdViewBags(mdBLL);
            //ViewBag.Employees = GetEmployeesSelectList();
            var model = new InterventionViewModel
            {
                Solutions = ViewBag.MdInterventionSolutions != null
                ? new SelectList(ViewBag.MdInterventionSolutions, "Value", "Text")
                : new SelectList(Enumerable.Empty<SelectListItem>()),
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
                        DebtAmount = viewModel.DebtAmount.HasValue ? _interventionBLL.EncryptDebtAmount(viewModel.DebtAmount.Value) : null,
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

            IMDBLL mdBLL = base.GetBLL<IMDBLL>();
            PopulateMdViewBags(mdBLL);
            //ViewBag.Employees = GetEmployeesSelectList();
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
                IMDBLL mdBLL = base.GetBLL<IMDBLL>();
                PopulateMdViewBags(mdBLL);

                viewModel.Solutions = ViewBag.MdInterventionSolutions != null
                    ? new SelectList(ViewBag.MdInterventionSolutions, "Value", "Text")
                    : new SelectList(Enumerable.Empty<SelectListItem>());
                return View(viewModel);
            }
            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, InterventionViewModel viewModel)
        {
            if (id != viewModel.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var response = base.bll.GetIntervention(id);
                if (response.Succeeded && response.Element != null)
                {
                    var existingIntervention = response.Element;

                    if (existingIntervention == null)
                    {
                        return NotFound();
                    }

                    //var intervention = MapToBOL(viewModel);

                    //Mettre à jour les solutions d'intervention
                    if (viewModel.SelectedSolutions != null)
                    {
                        // Suppression des anciennes solutions
                        var solutionsToRemove = existingIntervention.InterventionsInterventionSolutions.ToList();
                        foreach (var solution in solutionsToRemove)
                        {
                            _solutionsBLL.DeleteInterventionsInterventionSolutions(solution.Id);
                        }

                        // Creer les nouvelles solutions
                        foreach (var solutionId in viewModel.SelectedSolutions)
                        {
                            var solutionBOL = new InterventionsInterventionSolutionsBOL
                            {
                                IdIntervention = existingIntervention.Id,
                                IdInterventionSolution = solutionId
                            };
                            _solutionsBLL.CreateInterventionsInterventionSolutions(solutionBOL);
                        }

                    }

                    existingIntervention.IsVirtual = viewModel.IsVirtual;
                    existingIntervention.DateIntervention = viewModel.DateIntervention;
                    existingIntervention.IdEmployee = viewModel.IdEmployee;
                    existingIntervention.IdClient = viewModel.IdClient;
                    existingIntervention.IdReferenceType = viewModel.IdReferenceType;
                    existingIntervention.IdStatusType = viewModel.IdStatusType;
                    existingIntervention.IdInterventionType = viewModel.IdInterventionType;
                    existingIntervention.DebtAmount = viewModel.DebtAmount.HasValue ? _interventionBLL.EncryptDebtAmount(viewModel.DebtAmount.Value) : null;
                    existingIntervention.IdLoanReason = viewModel.IdLoanReason;
                    existingIntervention.IsLoanPaid = viewModel.IsLoanPaid;

                    base.bll.UpdateIntervention(existingIntervention);
                    TempData["success"] = "Intervention modifié avec succès";
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            IMDBLL mdBLL = base.GetBLL<IMDBLL>();
            PopulateMdViewBags(mdBLL);
            ViewBag.Employees = _employeeBLL.GetEmployeesSelectList();
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

        private InterventionViewModel MapToViewModel(InterventionBOL bol)
        {
            var employeeName = bol.IdEmployee.HasValue ? _employeeBLL.GetEmployeeName(bol.IdEmployee.Value) : "Inconnu";
            var clientName = bol.IdClient.HasValue ? _clientBLL.GetClientName(bol.IdClient.Value) : "Inconnu";
            var statusName = bol.IdStatusType.HasValue ? _mdbBLL.GetMdInterventionStatusTypeName(bol.IdStatusType.Value) : "Inconnu";         
            var interventionTypeName = bol.IdInterventionType.HasValue ? _mdbBLL.GetMdInterventionTypeName(bol.IdInterventionType.Value) : "Inconnu";
            var referenceTypeName = bol.IdReferenceType.HasValue ? _mdbBLL.GetReferenceTypeName(bol.IdReferenceType.Value) : "Inconnu";

            var solutionNames = bol.InterventionsInterventionSolutions != null
                ? bol.InterventionsInterventionSolutions.Select(s => _mdbBLL.GetSolutionName(s.IdInterventionSolution)).ToList()
                : new List<string>();


            return new InterventionViewModel
            {
                Id = bol.Id,
                IsVirtual = bol.IsVirtual,
                DateIntervention = bol.DateIntervention,
                IdEmployee = bol.IdEmployee,
                EmployeeName = employeeName,
                IdClient = bol.IdClient,
                ClientName = clientName,
                IdReferenceType = bol.IdReferenceType,
                ReferenceTypeName = referenceTypeName,
                IdStatusType = bol.IdStatusType,
                StatusName = statusName,
                IdInterventionType = bol.IdInterventionType,
                InterventionTypeName = interventionTypeName,
                DebtAmount = bol.DebtAmount != null ? _interventionBLL.DecryptDebtAmount(bol.DebtAmount) : (decimal?)null,
                IdLoanReason = bol.IdLoanReason,
                IsLoanPaid = bol.IsLoanPaid,
                SelectedSolutions = bol.InterventionsInterventionSolutions?.Select(s => s.IdInterventionSolution).ToList(),
                SolutionNames = solutionNames,
                
                  
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
                DebtAmount = viewModel.DebtAmount.HasValue ? _interventionBLL.EncryptDebtAmount(viewModel.DebtAmount.Value) : null,
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

        private void PopulateMdViewBags(IMDBLL mdBLL)
        {
            var mdReferenceSources = mdBLL.GetAllMdReferenceSources();
            var mdInterventionStatusTypes = mdBLL.GetAllMdInterventionStatusTypes();
            var mdInterventionTypes = mdBLL.GetAllMdInterventionTypes();
            var mdLoanReasons = mdBLL.GetAllMdLoanReasons();
            var mdInterventionSolutions = mdBLL.GetAllMdInterventionSolutions();
            var employees = GetEmployeesSelectList();



            ViewBag.MdReferenceSources = mdReferenceSources.ElementList.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            });

            ViewBag.MdInterventionStatusTypes = mdInterventionStatusTypes.ElementList.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            });

            ViewBag.MdInterventionTypes = mdInterventionTypes.ElementList.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            });

            ViewBag.MdLoanReasons = mdLoanReasons.ElementList.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            });

            ViewBag.MdInterventionSolutions = mdInterventionSolutions.ElementList.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            });

            ViewBag.Employees = employees;
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
                c.FullName.ToLower().Contains(searchTerm.ToLower()) ||
                c.Id.ToString().Contains(searchTerm)).ToList();

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

        //private DateTime StartOfWeek(DateTime dt, DayOfWeek startOfWeek)
        //{
        //    int diff = (7 + (dt.DayOfWeek - startOfWeek)) % 7;
        //    return dt.AddDays(-1 * diff).Date;

        //}


        // Méthodes de chiffrement et déchiffrement
        //    private byte[] EncryptDebtAmount(decimal amount)
        //    {
        //        return BitConverter.GetBytes((double)amount);
        //    }

        //    private decimal DecryptDebtAmount(byte[] encryptedAmount)
        //    {
        //        return (decimal)BitConverter.ToDouble(encryptedAmount, 0);
        //    }

        private string GetEmployeeName(int employeeId)
        {
            var employeeResponse = _employeeBLL.GetEmployeeById(employeeId);
            return employeeResponse.Succeeded && employeeResponse.Element != null
                ? $"{employeeResponse.Element.FirstName} {employeeResponse.Element.LastName}"
                : "Inconnu";
        }

        //    private string GetClientName(int clientId)
        //    {
        //        var clientResponse = _clientBLL.GetClient(clientId);
        //        return clientResponse.Succeeded && clientResponse.Element != null
        //            ? $"{clientResponse.Element.FirstName} {clientResponse.Element.LastName}"
        //            : "Inconnu";
        //    }

        //    private string GetStatusName(int statusId)
        //    {
        //        var statusResponse = _mdbBLL.GetMdInterventionStatusType(statusId);
        //        return statusResponse.Succeeded && statusResponse.Element != null
        //            ? statusResponse.Element.Name
        //            : "Inconnu";
        //    }

        //    private string GetReferenceTypeName(int referenceTypeId)
        //    {
        //        var referenceResponse = _mdbBLL.GetMdReferenceSource(referenceTypeId);
        //        return referenceResponse.Succeeded && referenceResponse.Element != null
        //            ? referenceResponse.Element.Name
        //            : "Inconnu";
        //    }

        //    private string GetInterventionTypeName(int interventionTypeId)
        //    {
        //        var interventionResponse = _mdbBLL.GetMdInterventionType(interventionTypeId);
        //        return interventionResponse.Succeeded && interventionResponse.Element != null
        //            ? interventionResponse.Element.Name
        //            : "Inconnu";
        //    }

        //    private string GetSolutionName(int solutionId)
        //    {
        //        var solutionResponse = _mdbBLL.GetMdInterventionSolution(solutionId);
        //        return solutionResponse.Succeeded && solutionResponse.Element != null
        //            ? solutionResponse.Element.Name
        //            : "Inconnu";
        //    }
    }
}
