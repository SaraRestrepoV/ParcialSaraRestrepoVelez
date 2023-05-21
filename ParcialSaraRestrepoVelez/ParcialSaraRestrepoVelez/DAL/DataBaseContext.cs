using Microsoft.EntityFrameworkCore;
using ParcialSaraRestrepoVelez.DAL.Entities;
using System.Diagnostics.Metrics;

namespace ParcialSaraRestrepoVelez.DAL
{
    public class DataBaseContext : DbContext
    {

        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options)
        { 
        
        }
        public DbSet<Ticket> Tickets { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Ticket>().HasIndex(c => c.Id).IsUnique();
        }
    }
}
