using System.Collections.Generic;
using DataContext.Models;
using Integrations.Models;

namespace Web.Models.Home
{
    public class HomeIndexModel
    {
        public List<Announcement> Announcements { get; set; }
        public List<Product> Products { get; set; }
        public List<RepositoryModel> Repositories { get; set; }
        public List<Event> Events { get; set; }
    }
}