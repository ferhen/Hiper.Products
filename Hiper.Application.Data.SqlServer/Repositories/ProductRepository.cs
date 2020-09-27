using Hiper.Application.Core.Models;
using Hiper.Application.Data.Repositories;
using Hiper.Application.Data.SqlServer.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiper.Application.Data.SqlServer.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context, ApplicationUser applicationUser) : base(context, applicationUser) { }

        public async Task<IEnumerable<Product>> ListIncludeStock()
        {
            return await DbSet
                .AsNoTracking()
                .Include(x => x.Stock)
                .ToListAsync();
        }

        public async Task<Product> GetByName(string name)
        {
            return await DbSet
                .AsNoTracking()
                .Include(x => x.Stock)
                .Where(x => x.Name == name)
                .FirstOrDefaultAsync();
        }

        public async Task<Product> GetByIdIncludeStock(int id)
        {
            return await DbSet
                .AsNoTracking()
                .Include(x => x.Stock)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();
        }
    }
}
