using EStore.Models;
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
        private readonly ApplicationDbContext _context;

        public DashboardController(ICategoryService service, IProductService productService, ApplicationDbContext context)
        {
            _context = context;
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

        public IActionResult Statistics()
        {
           
            var revenueData = new List<decimal>();
            var timePeriodLabels = new List<string>();

            // Giả sử bạn có một biến startDate và endDate để xác định khoảng thời gian
            var startDate = DateTime.Now.AddDays(-3);
            var endDate = DateTime.Now;

            // Tính tổng doanh thu cho mỗi ngày trong khoảng thời gian
            for (DateTime date = startDate; date <= endDate; date = date.AddDays(1))
            {
                var totalRevenue = _context.OrderDetails
                    .Where(od => od.Order.CreateDate.Date == date.Date)
                    .Sum(od => od.Quantity * od.UnitPrice);

                revenueData.Add((decimal)totalRevenue);
                timePeriodLabels.Add(date.ToString("dd/MM/yyyy"));
            }

            var statisticsViewModel = new StatisticsViewModel
            {
                RevenueData = revenueData,
                TimePeriodLabels = timePeriodLabels
            };

            return View(statisticsViewModel);
        }



    }
}
