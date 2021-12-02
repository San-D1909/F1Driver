using DataLayer;
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
    public class UpdateDatabase : IDisposable, IHostedService
    {
        private readonly IPopulateDatabase _populateDatabase;
        private Timer _timer;

        public UpdateDatabase( IPopulateDatabase populateDatabase)
        {
            _populateDatabase = populateDatabase;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _timer = new Timer(Populate, null, TimeSpan.Zero, TimeSpan.FromSeconds(30));
            return Task.CompletedTask;
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }
        private async void Populate(object state)
        {
            await _populateDatabase.Populate();
            Console.WriteLine("test");

        }
    }
}
