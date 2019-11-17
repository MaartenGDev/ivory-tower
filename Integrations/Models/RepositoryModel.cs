using System.Collections.Generic;

namespace Integrations.Models
{
    public class RepositoryModel
    {
        public string Name { get; set; }
        public bool IsHealthy { get; set; }
        public List<Commit> Commits { get; set; }
    }
}