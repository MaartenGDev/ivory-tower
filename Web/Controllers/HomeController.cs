using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DataContext.Data;
using Integrations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Web.Models;
using Web.Models.Home;

namespace Web.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ModuleService _moduleService;

        public HomeController(ApplicationDbContext context, ModuleService moduleService)
        {
            _context = context;
            _moduleService = moduleService;
        }

        public IActionResult Index()
        {
            var model = new HomeIndexModel
            {
                Announcements = _context.Announcements
                    .Include(x => x.Category)
                    .Include(x => x.User)
                    .OrderByDescending(x => x.PublishedAt)
                    .ToList(),
                Products = _context.Products
                    .Include(x => x.Tasks)
                    .ThenInclude(x => x.Status)
                    .ToList(),
                Repositories = _moduleService.GetRepositories()
            };
            
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}