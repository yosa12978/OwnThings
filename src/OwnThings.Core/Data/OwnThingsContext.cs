using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using OwnThings.Core.Models;
using OwnThings.Core.Seed;
using System;
using System.Collections.Generic;
using System.Text;

namespace OwnThings.Core.Data
{
    public class OwnThingsContext : DbContext
    {
        public ILogger<OwnThingsContext> _logger;
        public OwnThingsContext() { }
        public OwnThingsContext(DbContextOptions<OwnThingsContext> option, ILogger<OwnThingsContext> logger) : base(option)
        {
            _logger = logger;
            this.EnsureSeedData();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>().HasKey(m => m.id);
            modelBuilder.Entity<User>().Property(m => m.id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Device>().HasKey(m => m.id);
            modelBuilder.Entity<Device>().Property(m => m.id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Measurement>().HasKey(m => m.id);
            modelBuilder.Entity<Measurement>().Property(m => m.id).ValueGeneratedOnAdd();


            modelBuilder.Entity<Device>()
                .HasOne(m => m.owner)
                .WithMany(m => m.devices)
                .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Measurement>()
                .HasOne(m => m.device)
                .WithMany(m => m.measurements)
                .OnDelete(DeleteBehavior.NoAction);
        }
        public DbSet<Device> devices { get; set; }
        public DbSet<User> users { get; set; }
        public DbSet<Measurement> measurements { get; set; }
    }
}
