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
        public DefaultDbContext(IOptions<DatabaseOptions> dbOptions)
        {
            _dbOptions = dbOptions.Value;
            Database.EnsureCreated();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        
        {
            optionsBuilder.UseNpgsql($"Host={_dbOptions.Host};Port={_dbOptions.Port};Database={_dbOptions.DbName};Username={_dbOptions.Username};Password={_dbOptions.Password}");
        }
    }
}
