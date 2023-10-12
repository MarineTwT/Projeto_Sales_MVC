using Microsoft.AspNetCore.Mvc;
using Projeto_SalesMVC.Services;
using Projeto_SalesMVC.Models;
using Projeto_SalesMVC.Models.ViewModels;
using Projeto_SalesMVC.Services.Exceptions;
using System.Diagnostics;

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

        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> Create()
        {
            var departments = await _departmentsService.FindAllAsync();
            var ViewModel = new SellerFormViewModel { Departments = departments };
            return View(ViewModel);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not found" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not found" });
            }

            return View(obj);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            }

            var obj = await _sellerService.FindByIdAsync(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not found" });
            }

            List<Department> departments = await _departmentsService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments };

            return View(viewModel);
        }

        public IActionResult Error(string message)
        {
            var ViewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };

            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }

            catch(IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { Message = e.Message });
            }

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentsService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }           
            await _sellerService.insertAsync(seller);
            return RedirectToAction(nameof(Index));

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int Id, Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentsService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Seller = seller, Departments = departments };
                return View(viewModel);
            }

            if (Id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id miss match" });
            }

            try
            {
                await _sellerService.updateAsync(seller);
                return RedirectToAction(nameof(Index));
            }

            catch (NotFoundException e)
            {
                return RedirectToAction(nameof(Error), new { Message = e.Message });
            }

            catch (DBConcurrencyException e)
            {
                return RedirectToAction(nameof(Error), new { Message = e.Message });
            }
        }
    }
}
