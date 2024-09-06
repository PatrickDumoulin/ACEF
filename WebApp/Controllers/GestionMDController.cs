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
            // Récupération des détails de la MasterData par nom
            var mdDetails = base.bll.GetAllMdNamesByName(mdName);

            // Création du ViewModel à partir des détails récupérés
            var viewModel = new MdDetailViewModel
            {
                MdName = mdName,
                MdValues = mdDetails
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
                
            }

            return View(viewModel);
        }
    }
}
