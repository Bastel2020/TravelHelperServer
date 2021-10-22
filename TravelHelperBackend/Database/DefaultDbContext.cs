using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelHelperBackend.Database.Models;

namespace TravelHelperBackend.Database
{
    public class DefaultDbContext : DbContext
    {
        private readonly DatabaseOptions _dbOptions;
        public DbSet<User> Users { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public DbSet<TripDay> TripDays { get; set; }
        public DbSet<TripAction> TripActions { get; set; }
        public DbSet<City> Cities { get; set; }
        public DefaultDbContext(IOptions<DatabaseOptions> dbOptions)
        {
            _dbOptions = dbOptions.Value;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)   
        {
            optionsBuilder.UseNpgsql($"Host={_dbOptions.Host};Port={_dbOptions.Port};Database={_dbOptions.DbName};Username={_dbOptions.Username};Password={_dbOptions.Password}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.UserTrips)
                .WithMany(t => t.Members)
                .UsingEntity<TripMember>(
                   j => j
                    .HasOne(pt => pt.Trip)
                    .WithMany(t => t.MemberRoles)
                    .HasForeignKey(pt => pt.TripId),
                j => j
                    .HasOne(pt => pt.User)
                    .WithMany(p => p.TripRoles)
                    .HasForeignKey(pt => pt.UserId),
                j =>
                {
                    //j.Property(pt => pt.Role).HasDefaultValueSql(Enums.TripRolesEnum.Viewer.ToString()); ///ToString может не сработать
                    j.HasKey(t => new { t.TripId, t.UserId });
                    j.ToTable("TripMember");
                });

            modelBuilder.Entity<Trip>()
                .HasOne(t => t.TripDestination)
                .WithMany(c => c.PlannedTrips);

            modelBuilder.Entity<Trip>()
                .HasMany(t => t.TripDays)
                .WithOne(td => td.Parent);

            modelBuilder.Entity<TripDay>()
                .HasMany(td => td.Actions)
                .WithOne(ta => ta.Parent);

            modelBuilder.Entity<City>()
                .HasMany(c => c.PlannedTrips)
                .WithOne(t => t.TripDestination);
        }
    }
}
