using System.Collections.Generic;
using DataContext.Models;

namespace Web.Models
{
    public class HomeIndexModel
    {
        public List<Announcement> Announcements { get; set; }
    }
}