using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DataContext.Data;
using DataContext.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Models.Announcements;
using Web.Models.Products;

namespace Web.Controllers
{
    [Authorize]
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
            product.Tasks = product.Tasks.Select(task => new Task
            {
                Name = task.Name,
                Status = _context.TaskStatuses.Single(x => x.Id == task.Status.Id)
            }).ToList();

            _context.Products.Add(product);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int productId)
        {
            var model = new ManageProductModel
            {
                Product = _context.Products.Include(x => x.Tasks).ThenInclude(x => x.Status)
                    .Single(x => x.Id == productId),
                TaskStatuses = _context.TaskStatuses.ToList()
            };

            return View(model);
        }

        [HttpPost]
        public IActionResult Update(Product product, int productId)
        {
            var persistedProduct = _context.Products.Include(x => x.Tasks).Single(x => x.Id == productId);
            persistedProduct.Name = product.Name;

            var latestTasksForProduct = product.Tasks.Select(x => x.Id);
            var tasksToDelete = persistedProduct.Tasks.Where(x => !latestTasksForProduct.Contains(x.Id));

            _context.Tasks.RemoveRange(tasksToDelete);

            product.Tasks.ForEach(task =>
            {
                var isNewTask = task.Id < 1;

                var taskToPersist = isNewTask ? new Task() : _context.Tasks.Single(x => x.Id == task.Id);
                taskToPersist.Name = task.Name;
                taskToPersist.Status = _context.TaskStatuses.Single(x => x.Id == task.Status.Id);

                if (isNewTask)
                {
                    persistedProduct.Tasks.Add(taskToPersist);
                }

                var _ = isNewTask
                    ? _context.Tasks.Add(taskToPersist)
                    : _context.Tasks.Update(taskToPersist);
            });


            _context.SaveChanges();


            return RedirectToAction("Edit", new {productId});
        }

        public IActionResult Delete(int productId)
        {
            var product = _context.Products.Include(x => x.Tasks).Single(x => x.Id == productId);

            product.Tasks.Clear();
            
            _context.Products.Remove(product);
            _context.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}