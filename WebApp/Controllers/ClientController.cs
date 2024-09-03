using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Logic.Interfaces;
using DataAccess.BOL.Client;
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
    [Authorize(Roles = "Intervenant")]
    public class ClientController : AbstractBLLController<IClientBLL>
    {

        // GET: ClientController
        public IActionResult Index(ClientSearchViewModel searchModel, string sortOrder, int page = 1, int pageSize = 6)
        {
            var clients = base.bll.GetClients().ElementList;

            // Apply search filters
            if (searchModel.Id.HasValue)
            {
                clients = clients.Where(c => c.Id == searchModel.Id.Value).ToList();
            }
            if (!string.IsNullOrEmpty(searchModel.LastName))
            {
                clients = clients.Where(c => c.LastName.Contains(searchModel.LastName)).ToList();
            }
            if (!string.IsNullOrEmpty(searchModel.FirstName))
            {
                clients = clients.Where(c => c.FirstName.Contains(searchModel.FirstName)).ToList();
            }
            if (!string.IsNullOrEmpty(searchModel.PhoneNumber))
            {
                clients = clients.Where(c => c.PhoneNumber.Contains(searchModel.PhoneNumber)).ToList();
            }
            if (!string.IsNullOrEmpty(searchModel.Email))
            {
                clients = clients.Where(c => c.Email.Contains(searchModel.Email)).ToList();
            }

            // Apply sorting
            ViewBag.IdSortParm = String.IsNullOrEmpty(sortOrder) ? "id_desc" : "";
            ViewBag.LastNameSortParm = sortOrder == "LastName" ? "lastName_desc" : "LastName";
            ViewBag.FirstNameSortParm = sortOrder == "FirstName" ? "firstName_desc" : "FirstName";
            ViewBag.EmailSortParm = sortOrder == "Email" ? "email_desc" : "Email";

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
                CurrentPage = page,
                TotalPages = totalPages
            };
            return View(viewModel);
        }

        public IActionResult Details(int id)
        {
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
            return View();
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
                    Income = BitConverter.GetBytes(int.Parse(viewModel.Income))
                };

                base.bll.CreateClient(newClient);
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
        }

        private ClientViewModel MapToViewModel(IClientBOL client)
        {
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
                Income = BitConverter.ToInt32(client.Income, 0).ToString()
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
