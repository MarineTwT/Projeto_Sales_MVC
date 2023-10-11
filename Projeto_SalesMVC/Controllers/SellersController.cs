using Microsoft.AspNetCore.Mvc;

namespace Projeto_SalesMVC.Controllers
{
    public class SellersController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
