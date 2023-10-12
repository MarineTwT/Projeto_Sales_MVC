﻿using Microsoft.AspNetCore.Mvc;
using Projeto_SalesMVC.Services;
using Projeto_SalesMVC.Models;
using Projeto_SalesMVC.Models.ViewModels;
using Projeto_SalesMVC.Services.Exceptions;

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
                return NotFound();
            }
         
            var obj = _sellerService.FindById(id.Value);
            if(obj == null) 
            {
                return NotFound();
            }
          
            return View(obj);                          
        }

        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var obj = _sellerService.FindById(id.Value);
            if (obj == null)
            {
                return NotFound();
            }

            List<Department> departments = _departmentsService.FindAll();
            SellerFormViewModel viewModel = new SellerFormViewModel { Seller = obj, Departments = departments};

            return View(viewModel);
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
                return BadRequest();
            }


            else
            {
                try
                {
                    _sellerService.update(seller);
                    return RedirectToAction(nameof(Index));
                }

                catch (NotFoundException) 
                {
                    return NotFound();
                }

                catch(DBConcurrencyException)
                {
                    return BadRequest();
                }
            }
        }


    }
}
