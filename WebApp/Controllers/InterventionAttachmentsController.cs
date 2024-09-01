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

    //[Authorize(Roles = "Intervenant")]
    public class InterventionAttachmentsController : AbstractBLLController<IInterventionAttachmentBLL>
    {
        private readonly IEmployeeBLL _employeeBLL;

        private readonly UserManager<IdentityUser> _userManager;

        public InterventionAttachmentsController() : base()
        {
            _employeeBLL = Injector.ImplementBll<IEmployeeBLL>();

        }
        public IActionResult Index(int interventionId)
        {
            var response = bll.GetInterventionAttachmentsByInterventionId(interventionId);
            if (response.Succeeded)
            {
                ViewBag.InterventionId = interventionId;

                return View(response.ElementList);
            }
            return NotFound();
        }

        [HttpGet]
        public IActionResult Create(int interventionId)
        {
            ViewBag.InterventionId = interventionId;
            var model = new InterventionAttachmentViewModel
            {
                IdIntervention = interventionId
            };
            return View(model);

            
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InterventionAttachmentViewModel model, IFormFile file)
        {
            var currentUserName = HttpContext.User.Identity.Name;
            var employee = _employeeBLL.GetEmployeeByUsername(currentUserName).Element;

            if (file != null && file.Length > 0)
            {
                using (var stream = new MemoryStream())
                {
                    await file.CopyToAsync(stream);
                    var attachment = new InterventionAttachmentBOL
                    {
                        IdIntervention= model.IdIntervention,
                        IdEmployee = employee.Id, 
                        FileName = file.FileName,
                        FileContent = stream.ToArray(),
                        CreatedDate = DateTime.Now
                    };

                    bll.CreateInterventionAttachment(attachment);
                    return RedirectToAction(nameof(Index), new { interventionId = model.IdIntervention });
                }
            }

            return View(model);
        }

        public IActionResult Download(int id)
        {
            var response = bll.GetInterventionAttachmentById(id);
            if (response.Succeeded && response.Element != null)
            {
                var attachment = response.Element;
                return File(attachment.FileContent, "application/octet-stream", attachment.FileName);
            }
            return NotFound();
        }

        public IActionResult Delete(int id)
        {
            var response = bll.GetInterventionAttachmentById(id);
            if (response.Succeeded && response.Element != null)
            {
                var attachment = response.Element;
                bll.DeleteInterventionAttachment(id);
                return RedirectToAction(nameof(Index), new { interventionId = attachment.IdIntervention });
            }
            return NotFound();
        }
    }
}
