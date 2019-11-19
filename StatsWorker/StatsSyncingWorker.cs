using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using DataContext.Data;
using DataContext.Models;
using Integrations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Task = System.Threading.Tasks.Task;

namespace StatsWorker
{
    public class StatsSyncingWorker : BackgroundService
    {
        private const int MinutesBetweenSyncs = 60;
        private readonly ILogger<StatsSyncingWorker> _logger;
        private readonly ConfluenceService _confluenceService;
        private readonly IServiceScopeFactory _scopeFactory;

        public StatsSyncingWorker(ILogger<StatsSyncingWorker> logger, ConfluenceService confluenceService, IServiceScopeFactory scopeFactory)
        {
            _logger = logger;
            _confluenceService = confluenceService;
            _scopeFactory = scopeFactory;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                CacheConfluenceStats();
                
                const int delayInMilliseconds = MinutesBetweenSyncs * 60 * 60;
                await Task.Delay(delayInMilliseconds, stoppingToken);
            }
        }

        private void CacheConfluenceStats()
        {
            var contributors = _confluenceService.GetContributors();

            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                
                context.Contributors.RemoveRange(context.Contributors);
                context.Contributors.AddRange(contributors.Select(x => new Contributor
                {
                    Name = x.Name,
                    EditCount = x.EditCount,
                    CommentCount = x.CommentCount
                }));

                context.SaveChanges(); 
            }
    

        }
    }
}