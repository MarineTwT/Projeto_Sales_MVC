using Microsoft.AspNetCore.Mvc;
using Projeto_SalesMVC.Services;
using Projeto_SalesMVC.Models;

namespace Projeto_SalesMVC.Controllers
{
    public class SellersController : Controller
    {
        private readonly SellerService _sellerService;

        public SellersController(SellerService sellerService)
        {
            _sellerService = sellerService;
        }

        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        public IActionResult Create()
        {
            return View();
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
