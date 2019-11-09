using System.Collections.Generic;
using System.Linq;
using DataContext.Data;
using DataContext.Models;
using Microsoft.AspNetCore.Mvc;
using Web.Models;

namespace Web.Controllers
{
    public class AnnouncementController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AnnouncementController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            var model = new ManageAnnouncementModel
            {
                Announcement = new Announcement(),
                AnnouncementCategories = _context.AnnouncementCategories.ToList()
            };
            
            return View("Edit", model);
        }
        
        [HttpPost]
        public IActionResult Store(Announcement announcement)
        {
            _context.Announcements.Add(announcement);
            _context.SaveChanges();


            return RedirectToAction("Index", "Announcement");
        }
        
        [HttpGet]
        public IActionResult Edit(int announcementId)
        {
            var model = new ManageAnnouncementModel
            {
                Announcement = _context.Announcements.Single(x => x.Id == announcementId),
                AnnouncementCategories = _context.AnnouncementCategories.ToList()
            };
            
            return View(model);
        }
    }
}