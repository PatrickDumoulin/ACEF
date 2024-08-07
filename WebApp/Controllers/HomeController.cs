using BusinessLayer.Logic;
using BusinessLayer.Logic.Interfaces;
using CoreLib.Injection;
using CoreLib.Definitions;
using DataAccess;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Models;
using WebApp.Core.Controllers;
using DataModels.BOL;
using DataAccess.BOL;
using DataAccess.BOL.Production;
using DataModels.BOL.Production;
using BusinessLayer.Communication.Responses.Common;
using BusinessLayer.Communication.Responses.Interfaces;
using DataModels.BOL.Client;
using WebApp.ViewModels;

namespace WebApp.Controllers
{
	public class HomeController : AbstractBLLController<IClientBLL>
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			
			return View();

		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}

	//public class HomeController : AbstractBLLController<ICowMilkBLL>
 //   {
 //       private readonly ILogger<HomeController> _logger;

 //       public HomeController(ILogger<HomeController> logger)
 //       {
 //           _logger = logger;
 //       }

 //       public IActionResult Index()
 //       {
 //           IProductionBLL productionBLL = base.GetBLL<IProductionBLL>();
            
 //           GetItemResponse<ICowMilkIngredientBOL> response = base.bll.GetIngredient(2);
            
 //           CowMilkViewModel viewModel = new CowMilkViewModel();
 //           if (response.Succeeded && response.Element.UntypedRecord != null)
 //           {
 //               GetItemResponse<IProductionTypeDepartmentBOL> responseDepartment = productionBLL.GetProductionTypeDepartment(response.Element.IdProductionTypeDepartment);
 //               if(response.Succeeded && response.Element.UntypedRecord != null)
 //                   viewModel = new CowMilkViewModel(response.Element, responseDepartment.Element);
 //           }

 //           return View(viewModel);
 //       }

 //       public IActionResult Privacy()
 //       {
 //           return View();
 //       }

 //       [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
 //       public IActionResult Error()
 //       {
 //           return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
 //       }
 //   }
}