using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DataContext.Data;
using DataContext.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Models.Announcements;
using Web.Models.Products;

namespace Web.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProductController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new ProductIndexModel
            {
                Products = _context.Products
                    .Include(x => x.Tasks)
                    .ThenInclude(x => x.Status)
                    .ToList()
            };
            
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            var model = new ManageProductModel()
            {
                Product = new Product(),
                TaskStatuses = _context.TaskStatuses.ToList()
            };
            
            return View("Edit", model);
        }
        
        [HttpPost]
        public IActionResult Store(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();


            return RedirectToAction("Index", "Product");
        }
        
        [HttpGet]
        public IActionResult Edit(int productId)
        {
            var model = new ManageProductModel
            {
                Product = _context.Products.Single(x => x.Id == productId),
                TaskStatuses = _context.TaskStatuses.ToList()
            };
            
            return View(model);
        }
    }
}