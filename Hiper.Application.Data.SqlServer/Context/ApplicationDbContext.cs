using Hiper.Application.Core.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Security.Claims;

namespace Hiper.Application.Data.SqlServer
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<ApplicationUser>
    {
        public readonly string CurrentUser;

        public ApplicationDbContext(
            IHttpContextAccessor httpContextAccessor,
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
            CurrentUser = httpContextAccessor?.HttpContext?.User?.FindFirstValue(ClaimTypes.NameIdentifier);
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Stock> Stocks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException(nameof(modelBuilder));
            }

            // Foreign Keys
            modelBuilder.Entity<Stock>()
                .HasOne(stock => stock.Product)
                .WithOne(product => product.Stock)
                .HasForeignKey<Stock>(stock => stock.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Constraints
            modelBuilder.Entity<Product>()
                .HasIndex(d => d.Name)
                .IsUnique();

            base.OnModelCreating(modelBuilder);
        }
    }
}
