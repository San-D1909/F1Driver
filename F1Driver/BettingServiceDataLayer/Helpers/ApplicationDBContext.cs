using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BettingServiceDataLayer
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<ModelLayer.RaceModel> Race { get; set; }
        public DbSet<ModelLayer.NotificationModel> Notification { get; set; }
        public DbSet<ModelLayer.NotificationTypeModel> NotificationType { get; set; }
        public DbSet<ModelLayer.RaceResultModel> RaceResult { get; set; }
        public DbSet<ModelLayer.CircuitModel> Circuit { get; set; }
        public DbSet<ModelLayer.DriverModel> Driver { get; set; }
        public DbSet<ModelLayer.ConstructorModel> Constructor { get; set; }
        public DbSet<ModelLayer.UserModel> User { get; set; }
        public DbSet<ModelLayer.FriendGroupModel> FriendGroup { get; set; }
        public DbSet<ModelLayer.BetModel> Bet { get; set; }
    }
}

