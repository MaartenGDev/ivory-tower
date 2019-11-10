using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using DataContext.Data;
using DataContext.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models;
using Web.Models.Announcements;

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
            var model = new AnnouncementIndexModel
            {
                Announcements = _context.Announcements
                    .Include(x => x.Category)
                    .Include(x => x.User)
                    .ToList()
            };
            
            return View(model);
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
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            announcement.User = _context.Users.Single(x => x.Id == userId);
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