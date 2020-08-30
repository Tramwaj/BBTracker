using Microsoft.EntityFrameworkCore;
using BBTracker.Configurations;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using BBTracker.Models;
//using BBTracker.Models.PlayTypes;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BBTracker
{
    public class BBTrackerContext : DbContext
    {
        private readonly string _connectionString;
        public static readonly ILoggerFactory Logger = LoggerFactory.Create(builder => { builder.AddConsole(); });

        public DbSet<Player> Players { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Play> Plays { get; set; }
        public DbSet<PlayerGame> PlayerGames { get; set; }
        //public DbSet<Assist> Assists{ get; set; }
        //public DbSet<Block> Blocks { get; set; }
        //public DbSet<CheckIn> CheckIns { get; set; }
        //public DbSet<CheckOut> CheckOuts { get; set; }
        //public DbSet<FieldGoal> FieldGoals { get; set; }
        //public DbSet<Foul> Fouls { get; set; }
        //public DbSet<Rebound> Rebounds { get; set; }
        //public DbSet<Steal> Steals { get; set; }
        //public DbSet<Turnover> Turnovers { get; set; }

        public BBTrackerContext(string connectionString)
        {
            _connectionString = connectionString;
        }
        public BBTrackerContext()
        {
            _connectionString = "Server=.\\SQLEXPRESS;Database=BBTracker;Trusted_Connection=True";
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .UseSqlServer(_connectionString)
                .UseLoggerFactory(Logger);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Play>().ToTable("Plays");
            modelBuilder.ApplyConfiguration(new PlayerConfiguration());
            modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
        }

    }
}
