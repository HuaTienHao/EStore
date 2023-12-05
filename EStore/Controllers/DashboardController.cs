﻿using EStore.Models;
using Humanizer.Localisation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;

namespace EStore.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ICategoryService service;
        private readonly IProductService productService;

        public DashboardController(ICategoryService service, IProductService productService)
        {
            this.service = service;
            this.productService = productService;
        }
        [HttpGet]
        public IActionResult AddCategory()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category model)
        {
            var result = service.Add(model);
            if (result)
            {
                TempData["msg"] = "Added Successfully";
                return RedirectToAction("CategoryGetAll");
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }


        public IActionResult UpdateCategory(int id)
        {
            var record = service.FindById(id);
            return View(record);
        }

        [HttpPost]
        public IActionResult UpdateCategory(Category model)
        {
            
            var result = service.Update(model);
            if (result)
            {
                return RedirectToAction("CategoryGetAll");
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }


        public IActionResult Delete(int id)
        {

            var result = service.Delete(id);
            return RedirectToAction("CategoryGetAll");
        }

        public IActionResult CategoryGetAll(string searchTerm = "")
        {

            var data = service.GetAll(searchTerm);
            return View(data);
        }

        public IActionResult AddProduct()
        {
            var model = new Product();
            model.CategoryList = service.GetAll().Select(a => new SelectListItem { Text = a.CategoryName, Value = a.Id.ToString() }).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult AddProduct(Product model)
        {

            model.CategoryList = service.GetAll().Select(a => new SelectListItem { Text = a.CategoryName, Value = a.Id.ToString() }).ToList();
            
            var result = productService.Add(model);
            if (result)
            {
                
                TempData["msg"] = "Added Successfully";
                return RedirectToAction(nameof(AddProduct));
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }


        public IActionResult UpdateProduct(int id)
        {
            var model = productService.FindById(id);

            model.CategoryList = service.GetAll().Select(a => new SelectListItem { Text = a.CategoryName, Value = a.Id.ToString() }).ToList();
            return View(model);
        }

        [HttpPost]
        public IActionResult UpdateProduct(Product model)
        {
           
            model.CategoryList = service.GetAll().Select(a => new SelectListItem { Text = a.CategoryName, Value = a.Id.ToString() }).ToList();

            var result = productService.Update(model);
            if (result)
            {
                return RedirectToAction("ProductGetAll");
            }
            TempData["msg"] = "Error has occured on server side";
            return View(model);
        }


        public IActionResult DeleteProduct(int id)
        {

            var result = productService.Delete(id);
            return RedirectToAction("ProductGetAll");
        }

        public IActionResult ProductGetAll(string searchTerm = "")
        {

            var data = productService.GetAll(searchTerm);
            return View(data);
        }

    }
}
