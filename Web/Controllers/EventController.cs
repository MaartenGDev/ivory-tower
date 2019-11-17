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
using Web.Models.Events;

namespace Web.Controllers
{
    [Authorize]
    public class EventController : Controller
    {
        private readonly ApplicationDbContext _context;

        public EventController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var model = new EventIndexModel
            {
                Events = _context.Events.ToList()
            };
            
            return View(model);
        }
        
        [HttpGet]
        public IActionResult Create()
        {
            var model = new ManageEventModel
            {
                Event = new Event()
            };
            
            return View("Edit", model);
        }
        [HttpPost]
        public IActionResult Store(Event @event)
        {
            _context.Events.Add(@event);
            _context.SaveChanges();


            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public IActionResult Edit(int eventId)
        {
            var model = new ManageEventModel
            {
                Event = _context.Events.Single(x => x.Id == eventId),
            };
            
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Update(Event @event, int eventId)
        {
            var persistedEvent = _context.Events.Single(x => x.Id == eventId);
            persistedEvent.Name = @event.Name;
            persistedEvent.DateTime = @event.DateTime;

            _context.Events.Update(persistedEvent);
            _context.SaveChanges();
            
            return RedirectToAction("Edit", new {eventId});
        }
        
        [HttpGet]
        public IActionResult Delete(int eventId)
        {
            var @event = _context.Events.Single(x => x.Id == eventId);
            _context.Events.Remove(@event);

            _context.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}