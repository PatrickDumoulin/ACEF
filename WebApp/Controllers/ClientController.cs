using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Logic;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Definitions;
using CoreLib.Injection;
using DataAccess.BOL.Client;
using DataAccess.BOL.ClientIncomeType;
using DataAccess.BOL.InterventionsInterventionSolutions;
using DataAccess.Models;
using DataModels.BOL.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Linq;
using System.Text;
using WebApp.Core.Controllers;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
   
    public class ClientController : AbstractBLLController<IClientBLL>
    {
        private readonly IInterventionBLL _interventionBLL;
        private readonly IClientIncomeTypeBLL _incometypeBLL;
        private readonly IMDBLL _mdbBLL;
        private readonly ISeminarBLL _seminarBLL;
        

        public ClientController() : base()
        {
            _interventionBLL = Injector.ImplementBll<IInterventionBLL>();
            _mdbBLL = Injector.ImplementBll<IMDBLL>();
            _incometypeBLL = Injector.ImplementBll<IClientIncomeTypeBLL>();
        }

        // GET: ClientController
        public IActionResult Index(ClientSearchViewModel searchModel, string sortOrder, int page = 1, int pageSize = 6)
        {
            sortOrder = String.IsNullOrEmpty(sortOrder) ? "Id" : sortOrder;

            var clients = base.bll.GetClients().ElementList;
            var interventions = _interventionBLL.GetInterventions().ElementList;

            // Apply search filters
            if (searchModel.Id.HasValue)
            {
                clients = clients.Where(c => c.Id == searchModel.Id.Value).ToList();
            }
            if (!string.IsNullOrEmpty(searchModel.LastName))
            {
                clients = clients.Where(c => c.LastName.Contains(searchModel.LastName, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(searchModel.FirstName))
            {
                clients = clients.Where(c => c.FirstName.Contains(searchModel.FirstName, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (!string.IsNullOrEmpty(searchModel.PhoneNumber))
            {
                clients = clients.Where(c => c.PhoneNumber.Contains(searchModel.PhoneNumber)).ToList();
            }
            if (!string.IsNullOrEmpty(searchModel.Email))
            {
                clients = clients.Where(c => c.Email.Contains(searchModel.Email, StringComparison.OrdinalIgnoreCase)).ToList();
            }
            if (searchModel.isLoanPaid.HasValue)
            {
                // Filtrer les clients par le statut du prêt
                var clientIdsWithLoanStatus = interventions
                    .Where(i => i.IsLoanPaid == searchModel.isLoanPaid.Value)
                    .Select(i => i.IdClient) // Supposons qu'il y a une relation entre l'intervention et le client
                    .ToList();

                clients = clients.Where(c => clientIdsWithLoanStatus.Contains(c.Id)).ToList();
            }

            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "Id";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "lastName_desc" : "LastName";
            ViewBag.FirstNameSortParm = sortOrder == "FirstName" ? "firstName_desc" : "FirstName";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "email_desc" : "Email";
            ViewBag.CurrentSortColumn = sortOrder; // Set the current column being sorted
            ViewBag.CurrentSortOrder = sortOrder.EndsWith("_desc") ? "desc" : "asc"; // Set the current order

            switch (sortOrder)
            {
                case "id_desc":
                    clients = clients.OrderByDescending(c => c.Id).ToList();
                    break;
                case "LastName":
                    clients = clients.OrderBy(c => c.LastName).ToList();
                    break;
                case "lastName_desc":
                    clients = clients.OrderByDescending(c => c.LastName).ToList();
                    break;
                case "FirstName":
                    clients = clients.OrderBy(c => c.FirstName).ToList();
                    break;
                case "firstName_desc":
                    clients = clients.OrderByDescending(c => c.FirstName).ToList();
                    break;
                case "Email":
                    clients = clients.OrderBy(c => c.Email).ToList();
                    break;
                case "email_desc":
                    clients = clients.OrderByDescending(c => c.Email).ToList();
                    break;
                default:
                    clients = clients.OrderBy(c => c.Id).ToList();
                    break;
            }


            var totalcount = clients.Count;
            var totalPages = (int)Math.Ceiling((decimal)totalcount / pageSize);
            var pagedClients = clients.Skip((page - 1) * pageSize).Take(pageSize).ToList();

            var viewModel = new ClientSearchViewModel
            {
                Id = searchModel.Id,
                LastName = searchModel.LastName,
                FirstName = searchModel.FirstName,
                PhoneNumber = searchModel.PhoneNumber,
                Email = searchModel.Email,
                Clients = pagedClients.Select(MapToViewModel).ToList(),
                isLoanPaid = searchModel.isLoanPaid,  // Passer la valeur du prêt remboursé dans le ViewModel
                CurrentPage = page,
                TotalPages = totalPages
            };
            return View(viewModel);
        }

        public IActionResult Details(int id, int? clientId = null)
        {
            if (clientId.HasValue)
            {
                // Indique que l'utilisateur vient de la page de création du séminaire
                ViewBag.ReturnToSeminarCreation = true;
            }

            var response = base.bll.GetClient(id);
            if (response.Succeeded && response.Element != null)
            {
                var viewModel = MapToViewModel(response.Element);

                IMDBLL mdBLL = base.GetBLL<IMDBLL>();
                PopulateMdViewBags(mdBLL);

                ViewBag.MdBank = mdBLL.GetMdBank(viewModel.IdBank ?? 0)?.Element?.Name;
                ViewBag.MdEmploymentSituation = mdBLL.GetMdEmploymentSituation(viewModel.IdEmploymentSituation ?? 0)?.Element?.Name;
                ViewBag.MdMaritalStatus = mdBLL.GetMdMaritalStatus(viewModel.IdMaritalStatus ?? 0)?.Element?.Name;
                ViewBag.MdFamilySituation = mdBLL.GetMdFamilySituation(viewModel.IdFamilySituation ?? 0)?.Element?.Name;
                ViewBag.MdGenderDenomination = mdBLL.GetMdGenderDenomination(viewModel.IdGenderDenomination ?? 0)?.Element?.Name;
                ViewBag.MdHabitationType = mdBLL.GetMdHabitationType(viewModel.IdHabitationType ?? 0)?.Element?.Name;
                ViewBag.MdScholarshipType = mdBLL.GetMdScholarshipType(viewModel.IdScholarshipType ?? 0)?.Element?.Name;

                var noteCount = GetNoteCount(id);
                var attachmentCount = GetAttachmentCount(id);
                ViewBag.NoteCount = noteCount;
                ViewBag.AttachmentCount = attachmentCount;

                return View(viewModel);
            }

            return NotFound();
        }

        public ActionResult Create()
        {
            IMDBLL mdBLL = base.GetBLL<IMDBLL>();
            PopulateMdViewBags(mdBLL);
            //ViewBag.Employees = GetEmployeesSelectList();
            var model = new ClientViewModel
            {
                IncomeType = ViewBag.MdIncomeType != null
                ? new SelectList(ViewBag.MdIncomeType, "Value", "Text")
                : new SelectList(Enumerable.Empty<SelectListItem>()),
                SelectedIncomeType = new List<int>()
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ClientViewModel viewModel)
        {
            viewModel.FullName = viewModel.FirstName + " " + viewModel.LastName;
            ModelState.Remove("FullName"); // Ignore la validation du FullName

            if (ModelState.IsValid)
            {
                var newClient = new ClientBOL
                {
                    IsMember = viewModel.IsMember,
                    LastName = viewModel.LastName,
                    FirstName = viewModel.FirstName,
                    Birthdate = viewModel.Birthdate,
                    PhoneNumber = viewModel.PhoneNumber,
                    Email = viewModel.Email,
                    IdGenderDenomination = viewModel.IdGenderDenomination,
                    Address = viewModel.Address,
                    ZipCode = viewModel.ZipCode,
                    IdMaritalStatus = viewModel.IdMaritalStatus,
                    IdFamilySituation = viewModel.IdFamilySituation,
                    AdultsAtHome = viewModel.AdultsAtHome,
                    ChildsAtHome = viewModel.ChildsAtHome,
                    IdHabitationType = viewModel.IdHabitationType,
                    IdBank = viewModel.IdBank,
                    IdEmploymentSituation = viewModel.IdEmploymentSituation,
                    IdScholarshipType = viewModel.IdScholarshipType,
                    Income = !string.IsNullOrEmpty(viewModel.Income) ? BitConverter.GetBytes(int.Parse(viewModel.Income)) : null
                };

                base.bll.CreateClient(newClient);

                if (newClient.Id > 0 && viewModel.SelectedIncomeType != null && viewModel.SelectedIncomeType.Any())
                {
                    foreach (var incomeTypeId in viewModel.SelectedIncomeType)
                    {
                        var incomeTypeBOL = new ClientIncomeTypeBOL
                        {
                            IdClient = newClient.Id,
                            IdIncomeType = incomeTypeId
                        };
                        _incometypeBLL.CreateClientIncomeType(incomeTypeBOL);
                    }
                }
                TempData["success"] = "Client crée avec succès";
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        public IActionResult Edit(int id)
        {
            var response = base.bll.GetClient(id);
            if (response.Succeeded && response.Element != null)
            {
                var viewModel = MapToViewModel(response.Element);

                IMDBLL mdBLL = base.GetBLL<IMDBLL>();
                PopulateMdViewBags(mdBLL);

                viewModel.IncomeType = ViewBag.MdIncomeType != null
                    ? new SelectList(ViewBag.MdIncomeType, "Value", "Text")
                    : new SelectList(Enumerable.Empty<SelectListItem>());
                return View(viewModel);

                
            }

            return NotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, ClientViewModel viewModel)
        {
            viewModel.FullName = viewModel.FirstName + " " + viewModel.LastName;
            ModelState.Remove("FullName"); // Ignore la validation du FullName

            if (id != viewModel.Id)
            {
                return BadRequest();
            }

            if (ModelState.IsValid)
            {
                var response = base.bll.GetClient(id);
                if (response.Succeeded && response.Element != null)
                {
                    var existingClient = response.Element as ClientBOL;

                    if (existingClient == null)
                    {
                        return NotFound();
                    }

                    //Mettre à jour les incomeTypes
                    if (viewModel.SelectedIncomeType != null)
                    {
                        // Suppression des anciens incomeTypes
                        var incomeTypesToRemove = existingClient.ClientIncomeTypes.ToList();
                        foreach (var incomeType in incomeTypesToRemove)
                        {
                            _incometypeBLL.DeleteClientIncomeType(incomeType.Id);
                        }

                        // Creer un nouveau incomeType
                        foreach (var incomeTypeId in viewModel.SelectedIncomeType)
                        {
                            var incomeTypeBOL = new ClientIncomeTypeBOL
                            {
                                IdClient = existingClient.Id,
                                IdIncomeType = incomeTypeId
                            };
                            _incometypeBLL.CreateClientIncomeType(incomeTypeBOL);
                        }

                    }

                    existingClient.IsMember = viewModel.IsMember;
                    existingClient.LastName = viewModel.LastName;
                    existingClient.FirstName = viewModel.FirstName;
                    existingClient.Birthdate = viewModel.Birthdate;
                    existingClient.PhoneNumber = viewModel.PhoneNumber;
                    existingClient.Email = viewModel.Email;
                    existingClient.IdGenderDenomination = viewModel.IdGenderDenomination;
                    existingClient.Address = viewModel.Address;
                    existingClient.ZipCode = viewModel.ZipCode;
                    existingClient.IdMaritalStatus = viewModel.IdMaritalStatus;
                    existingClient.IdFamilySituation = viewModel.IdFamilySituation;
                    existingClient.AdultsAtHome = viewModel.AdultsAtHome;
                    existingClient.ChildsAtHome = viewModel.ChildsAtHome;
                    existingClient.IdHabitationType = viewModel.IdHabitationType;
                    existingClient.IdBank = viewModel.IdBank;
                    existingClient.IdEmploymentSituation = viewModel.IdEmploymentSituation;
                    existingClient.IdScholarshipType = viewModel.IdScholarshipType;
                    existingClient.Income = BitConverter.GetBytes(int.Parse(viewModel.Income));

                    base.bll.UpdateClient(existingClient);
                    TempData["success"] = "Client modifié avec succès";
                    return RedirectToAction(nameof(Index));
                }
                return NotFound();
            }
            return View(viewModel);
        }

        // GET: ClientController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClientController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        private void PopulateMdViewBags(IMDBLL mdBLL)
        {
            var mdBanks = mdBLL.GetAllMdBanks();
            var mdEmploymentSituations = mdBLL.GetAllMdEmploymentSituations();
            var mdMaritalStatus = mdBLL.GetAllMdMaritalStatus();
            var mdFamilySituation = mdBLL.GetAllMdFamilySituations();
            var mdGenderDenomination = mdBLL.GetAllMdGenderDenominations();
            var mdHabitationType = mdBLL.GetAllMdHabitationTypes();
            var mdScholarshipType = mdBLL.GetAllMdScholarshipTypes();
            var mdIncomeType = mdBLL.GetAllMdIncomeTypes();

            ViewBag.MdBanks = mdBanks.ElementList.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            });

            ViewBag.MdEmploymentSituations = mdEmploymentSituations.ElementList.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            });

            ViewBag.MdMaritalStatus = mdMaritalStatus.ElementList.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            });

            ViewBag.MdFamilySituation = mdFamilySituation.ElementList.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            });

            ViewBag.MdGenderDenomination = mdGenderDenomination.ElementList.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            });

            ViewBag.MdHabitationType = mdHabitationType.ElementList.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            });

            ViewBag.MdScholarshipType = mdScholarshipType.ElementList.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name
            });

            ViewBag.MdIncomeType = mdIncomeType.ElementList.Select(b => new SelectListItem
            {
                Value = b.Id.ToString(),
                Text = b.Name   
            });
        }

        private ClientViewModel MapToViewModel(IClientBOL client)
        {
            var intervention = _interventionBLL.GetInterventions().ElementList.FirstOrDefault(i => i.IdClient == client.Id);
            var incomeTypeNames = client.ClientIncomeTypes != null
                ? client.ClientIncomeTypes.Select(r => _mdbBLL.GetIncomeTypeName(r.IdIncomeType)).ToList()
                : new List<string>();

            return new ClientViewModel
            {
                Id = client.Id,
                IsMember = client.IsMember,
                LastName = client.LastName,
                FirstName = client.FirstName,
                FullName = client.FullName,
                Birthdate = client.Birthdate,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                IdGenderDenomination = client.IdGenderDenomination,
                Address = client.Address,
                ZipCode = client.ZipCode,
                IdMaritalStatus = client.IdMaritalStatus,
                IdFamilySituation = client.IdFamilySituation,
                AdultsAtHome = client.AdultsAtHome,
                ChildsAtHome = client.ChildsAtHome,
                IdHabitationType = client.IdHabitationType,
                IdBank = client.IdBank,
                IdEmploymentSituation = client.IdEmploymentSituation,
                IdScholarshipType = client.IdScholarshipType,
                Income = BitConverter.ToInt32(client.Income, 0).ToString(),
                IsLoanPaid = intervention?.IsLoanPaid, // Null si aucune intervention trouvée
                SelectedIncomeType = client.ClientIncomeTypes?.Select(s => s.IdIncomeType).ToList(),
                IncomeTypeNames = incomeTypeNames,
            };
        }

        private int GetNoteCount(int clientId)
        {
            var noteBLL = GetBLL<INoteBLL>();
            return noteBLL.GetNotesByClientId(clientId).ElementList.Count();
        }

        private int GetAttachmentCount(int clientId)
        {
            var attachmentBLL = GetBLL<IClientAttachmentBLL>();
            return attachmentBLL.GetAttachmentsByClientId(clientId).ElementList.Count();
        }
    }
}
