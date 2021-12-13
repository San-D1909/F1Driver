using DataLayer;
using DataLayer.Classes;
using DataLayer.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace F1DriverBack.Services
{
    public class UpdateDatabaseOnTimer : IDisposable, IHostedService
    {
        private Timer _timer;
        private readonly IServiceScopeFactory _scopeFactory;


        public UpdateDatabaseOnTimer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Populate, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        private async void Populate(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                IPopulateDatabase populateClass = new PopulateDatabase(context);
                await populateClass.Populate();
            }
            Console.WriteLine("Database update succes");

        }
    }
}
