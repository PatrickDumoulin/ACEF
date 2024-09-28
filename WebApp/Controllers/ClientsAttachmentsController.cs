using BusinessLayer.Logic.Interfaces;
using DataAccess.BOL.ClientAttachment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;
using WebApp.ViewModels;
using CoreLib.Definitions;
using WebApp.Core.Controllers;
using Microsoft.AspNetCore.Authorization;
using DataAccess.BOL.InterventionAttachment;
using CoreLib.Injection;
using Microsoft.AspNetCore.Identity;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Superutilisateur, Intervenant")]
    public class ClientsAttachmentsController : AbstractBLLController<IClientAttachmentBLL>
    {
        private readonly IEmployeeBLL _employeeBLL;

        private readonly UserManager<IdentityUser> _userManager;

        public ClientsAttachmentsController() : base()
        {
            _employeeBLL = Injector.ImplementBll<IEmployeeBLL>();

        }
        public IActionResult Index(int clientId)
        {
            var response = bll.GetAttachmentsByClientId(clientId);
            if (response.Succeeded)
            {
                ViewBag.ClientId = clientId;
                return View(response.ElementList);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Create(int clientId)
        {
            ViewBag.ClientId = clientId;
            var model = new ClientAttachmentViewModel
            {
                ClientId = clientId
            };
            return View(model);


        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientAttachmentViewModel model, IFormFile file)
        {
            var currentUserName = HttpContext.User.Identity.Name;
            var employee = _employeeBLL.GetEmployeeByUsername(currentUserName).Element;

            if (file != null && file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    var attachment = new ClientAttachmentBOL
                    {
                        IdClient = model.ClientId,
                        IdEmployee = employee.Id,
                        FileName = file.FileName,
                        FileContent = stream.ToArray(),
                        CreatedDate = DateTime.Now
                    };

                    bll.CreateAttachment(attachment);
                    TempData["success"] = "Pièce Jointe Créee avec succès";
                    return RedirectToAction(nameof(Index), new { clientId = model.ClientId});
                }
            }

            return View(model);
        }

        public IActionResult Download(int id)
        {
            var response = bll.GetAttachmentById(id);
            if (response.Succeeded && response.Element != null)
            {
                var attachment = response.Element;
                return File(attachment.FileContent, "application/octet-stream", attachment.FileName);
            }
            return NotFound();
        }

        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var response = bll.GetAttachmentById(id);
            if (response.Succeeded && response.Element != null)
            {
                var attachment = response.Element;
                bll.DeleteAttachment(id);

                return RedirectToAction(nameof(Index), new { clientId = attachment.IdClient });
            }
            TempData["Error"] = "La suppression de la pièce jointe a échouée";
            return NotFound();
        }
    }
}
