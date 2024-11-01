﻿using BusinessLayer.Logic;
using BusinessLayer.Logic.Interfaces;
using DataAccess.BOL.Client;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using WebApp.Core.Controllers;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
    [Authorize(Roles = "Superutilisateur, Intervenant")]
    public class GestionMDController : AbstractBLLController<IMDBLL>
    {
        public IActionResult Index(string sortColumn, string sortOrder)
        {
            // Récupérer le dictionnaire des Master Data
            var masterDatas = base.bll.GetAllMDNames();

            // Passer les informations de tri à la vue via ViewBag
            sortColumn = string.IsNullOrEmpty(sortColumn) ? "Valeur" : sortColumn;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;

            // Passer les informations de tri à la vue
            ViewBag.CurrentSortColumn = sortColumn;
            ViewBag.CurrentSortOrder = sortOrder;

            // Convertir le dictionnaire en une collection triable
            var sortedMasterDatas = masterDatas.AsEnumerable();

            // Appliquer le tri
            switch (sortColumn)
            {
                case "Valeur":
                    sortedMasterDatas = sortOrder == "asc"
                        ? masterDatas.OrderBy(i => i.Value)  // Tri ascendant par valeur
                        : masterDatas.OrderByDescending(i => i.Value);  // Tri descendant par valeur
                    break;
            }

            // Créer le ViewModel avec les données triées
            var viewModel = new MdGestionIndexViewModel
            {
                MdNames = sortedMasterDatas.Select(kv => kv.Value).ToList()  // Récupérer les valeurs triées
            };

            return View(viewModel);
        }

        public IActionResult Details(string mdName, string sortColumn, string sortOrder)
        {

            // Récupérer le dictionnaire des Master Data
            var masterDataItems = base.bll.GetAllMasterDataItems()[mdName];

            // Passer les informations de tri à la vue via ViewBag
            sortColumn = string.IsNullOrEmpty(sortColumn) ? "Valeur" : sortColumn;
            sortOrder = string.IsNullOrEmpty(sortOrder) ? "asc" : sortOrder;

            // Passer les informations de tri à la vue
            ViewBag.CurrentSortColumn = sortColumn;
            ViewBag.CurrentSortOrder = sortOrder;

            // Convertir le dictionnaire en une collection triable
            var sortedMasterDatas = masterDataItems.AsEnumerable();

            // Appliquer le tri
            switch (sortColumn)
            {
                case "Valeur":
                    sortedMasterDatas = sortOrder == "asc"
                        ? masterDataItems.OrderBy(i => i.Name)  // Tri ascendant par valeur
                        : masterDataItems.OrderByDescending(i => i.Name);  // Tri descendant par valeur
                    break;
            }

            // Créer le ViewModel avec les données triées
            var viewModel = new MdDetailViewModel
            {
                MdName = mdName,
                MdItems = sortedMasterDatas.ToList()  // Liste d'objets MasterDataViewModel
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
                bool isDesjardins = false;
                base.bll.CreateMasterDataItem(mdName, viewModel.Name, viewModel.Active,viewModel.IsDesjardins);
                TempData["success"] = "Donnée créée avec succès";
                return RedirectToAction(nameof(Details), new { mdName = mdName });
            }

            return View(viewModel);
        }

        public IActionResult Edit(string oldName, string mdName, int id)
        {
            var item = base.bll.GetAllMasterDataItems()[mdName].FirstOrDefault(x => x.Id == id);
            if (item == null)
            {
                return NotFound();
            }

            var viewModel = new MdItemViewModel
            {
                Name = item.Name,
                IsDesjardins = item.IsDesjardins
            };

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
                bool isDesjardins = false;
                base.bll.EditMasterDataItem(mdName, oldName, viewModel.Name, viewModel.Active, viewModel.IsDesjardins);
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
            bool isDesjardins = false;

            base.bll.EditMasterDataItem(mdName, mdItemName, mdItemName, newActiveState, isDesjardins);
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
