using System;
using System.Collections.Generic;
using Integrations.Models;

namespace Integrations
{
    public class ModuleService
    {
        public List<RepositoryModel> GetRepositories()
        {
            return new List<RepositoryModel>
            {
                new RepositoryModel
                {
                    Name = "Sending Messages",
                    IsHealthy = true,
                    Commits = new List<Commit>
                    {
                        new Commit
                        {
                            Hash = "248e6d3",
                            Message = "Initial Commit"
                        }
                    }
                },
                new RepositoryModel
                {
                    Name = "Creating Messages",
                    IsHealthy = false,
                    Commits = new List<Commit>
                    {
                        new Commit
                        {
                            Hash = "015882a",
                            Message = "Initial Commit"
                        }
                    }
                },
                new RepositoryModel
                {
                    Name = "Discovering Clients",
                    IsHealthy = true,
                    Commits = new List<Commit>
                    {
                        new Commit
                        {
                            Hash = "e82c6df",
                            Message = "Initial Commit"
                        }
                    }
                }
            };
        }
    }
}