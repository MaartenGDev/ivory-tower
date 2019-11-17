using System.Collections.Generic;
using DataContext.Models;
using Integrations.Models;
using Contributor = DataContext.Models.Contributor;

namespace Web.Models.Home
{
    public class HomeIndexModel
    {
        public List<Announcement> Announcements { get; set; }
        public List<Product> Products { get; set; }
        public List<RepositoryModel> Repositories { get; set; }
        public List<Event> Events { get; set; }
        public IEnumerable<Contributor> Contributors { get; set; }
    }
}