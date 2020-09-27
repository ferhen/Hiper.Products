using Hiper.SynchronizationAPI.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace Hiper.SynchronizationAPI.Data.SqlServer.Context
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options) { }

        public DbSet<ProductSnapshot> ProductSnapshots { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            // Constraints
            modelBuilder.Entity<ProductSnapshot>()
                .HasIndex(d => d.Name)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
