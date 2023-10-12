using Microsoft.AspNetCore.Mvc;
using Projeto_SalesMVC.Services;
using Projeto_SalesMVC.Models;
using Projeto_SalesMVC.Models.ViewModels;
using Projeto_SalesMVC.Services.Exceptions;
using System.Diagnostics;
using Microsoft.DotNet.Scaffolding.Shared.Messaging;

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

        public IActionResult Delete(int? id) 
        {
            if(id == null)
            {
                return RedirectToAction(nameof(Error), new {Message = "Id not provided"});
            }
         
            var obj = _sellerService.FindById(id.Value);
            if(obj == null) 
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not found" });
            }
          
            return View(obj);                          
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not found" });
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not provided" });
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id not found" });
            }

            List<Department> departments = _departmentsService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments};

            return View(viewModel);
        }

        public IActionResult Error(string message)
        {
            var ViewModel = new ErrorViewModel { Message = message, RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };

            return View(ViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _sellerService.Remove(id);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller) 
        {
            _sellerService.insert(seller);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id,Seller seller)
        { 
            if(Id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { Message = "Id miss match" });
            }


            else
            {
                try
                {
                    _sellerService.update(seller);
                    return RedirectToAction(nameof(Index));
                }

                catch (NotFoundException e) 
                {
                    return RedirectToAction(nameof(Error), new { Message = e.Message});
                }

                catch(DBConcurrencyException e)
                {
                    return RedirectToAction(nameof(Error), new { Message = e.Message});
                }
            }
        }
    }
}
