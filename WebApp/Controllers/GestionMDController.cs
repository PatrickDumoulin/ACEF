using BusinessLayer.Logic;
using BusinessLayer.Logic.Interfaces;
using DataAccess.BOL.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Core.Controllers;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    public class GestionMDController : AbstractBLLController<IMDBLL>
    {
        public IActionResult Index()
        {
            var masterDatas = base.bll.GetAllMDNames();

            var viewModel = new MdGestionIndexViewModel
            {
                MdNames = masterDatas.Values.ToList(),
            };
            return View(viewModel);

        }

        public IActionResult Details(string mdName)
        {
            // Récupérer les détails des objets Master Data
            var masterDataItems = base.bll.GetAllMasterDataItems()[mdName];

            var viewModel = new MdDetailViewModel
            {
                MdName = mdName,
                MdItems = masterDataItems.ToList()  // Liste d'objets MasterDataViewModel
            };

            return View(viewModel);
        }

        public IActionResult Create(string name)
        {
            var viewModel = new MdItemViewModel();
            ViewData["MdName"] = name;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(MdItemViewModel viewModel, string mdName)
        {
            viewModel.Active = true;

            if (ModelState.IsValid)
            {
                base.bll.CreateMasterDataItem(mdName, viewModel.Name, viewModel.Active);
                TempData["success"] = "Donnée créée avec succès";
                return RedirectToAction(nameof(Details), new { mdName = mdName });
            }

            return View(viewModel);
        }

        public IActionResult Edit(string oldName, string mdName)
        {

            var viewModel = new MdItemViewModel();

            ViewData["MdName"] = mdName;
            ViewData["OldName"] = oldName;
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(MdItemViewModel viewModel, string mdName, string oldName)
        {

            viewModel.Active = true;



            if (ModelState.IsValid)
            {
                base.bll.EditMasterDataItem(mdName, oldName, viewModel.Name, viewModel.Active);
                TempData["success"] = "Donnée modifiée avec succès";
                return RedirectToAction("Details", new { mdName = mdName });
            }

            return View(viewModel);
        }

        public IActionResult ToggleActive(string mdName, string mdItemName)
        {
            var masterDataItems = base.bll.GetAllMasterDataItems()[mdName];

            var item = masterDataItems.FirstOrDefault(i => i.Name == mdItemName);

            if (item == null)
            {
                return NotFound(); 
            }

            bool? newActiveState = !item.IsActive;

            base.bll.EditMasterDataItem(mdName, mdItemName, mdItemName, newActiveState);
            TempData["success"] = "Statut changé avec succès";

            return RedirectToAction("Details", new { mdName = mdName });


        }
        [HttpDelete]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(string mdName, string mdItemName)
        {
            base.bll.DeleteMasterDataItem(mdName, mdItemName);

            return RedirectToAction("Details", new { mdName = mdName });

        }
    }
}
