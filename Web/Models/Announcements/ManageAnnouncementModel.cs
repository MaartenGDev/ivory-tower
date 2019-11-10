using System.Collections.Generic;
using DataContext.Models;

namespace Web.Models.Announcements
{
    public class ManageAnnouncementModel
    {
        public Announcement Announcement { get; set; }
        public List<AnnouncementCategory> AnnouncementCategories { get; set; }
    }
}