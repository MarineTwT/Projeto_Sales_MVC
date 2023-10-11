using Microsoft.AspNetCore.Mvc;
using Projeto_SalesMVC.Services;
using Projeto_SalesMVC.Models;
using Projeto_SalesMVC.Models.ViewModels;

namespace Projeto_SalesMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;
        private readonly DepartmentsService _departmentsService;

        public SellersController(SellerService sellerService, DepartmentsService departmentsService)
        {
            _sellerService = sellerService;
            _departmentsService = departmentsService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            var departments = _departmentsService.FindAll();
            var ViewModel = new SellerFormViewModel { Departments = departments };
            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) 
        {
            _sellerService.insert(seller);
            return RedirectToAction(nameof(Index));
        }
    }
}
