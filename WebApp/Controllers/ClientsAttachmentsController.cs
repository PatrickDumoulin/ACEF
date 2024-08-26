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

namespace WebApp.Controllers
{
    [Authorize(Roles = "Intervenant")]
    public class ClientsAttachmentsController : AbstractBLLController<IClientAttachmentBLL>
    {
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
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ClientAttachmentViewModel model, IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    var attachment = new ClientAttachmentBOL
                    {
                        IdClient = model.ClientId,
                        IdEmployee = null, // Remplacez par l'ID de l'employé connecté si nécessaire
                        FileName = file.FileName,
                        FileContent = stream.ToArray(),
                        CreatedDate = DateTime.Now
                    };

                    bll.CreateAttachment(attachment);
                    return RedirectToAction(nameof(Index), new { clientId = model.ClientId });
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

        public IActionResult Delete(int id)
        {
            var response = bll.GetAttachmentById(id);
            if (response.Succeeded && response.Element != null)
            {
                var attachment = response.Element;
                bll.DeleteAttachment(id);
                return RedirectToAction(nameof(Index), new { clientId = attachment.IdClient });
            }
            return NotFound();
        }
    }
}
