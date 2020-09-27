using Hiper.SynchronizationAPI.Core.Models;
using Hiper.SynchronizationAPI.Data.Repositories;
using Hiper.SynchronizationAPI.Data.SqlServer.Context;
using Hiper.SynchronizationAPI.Data.SqlServer.Repositories.Base;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Hiper.SynchronizationAPI.Data.SqlServer.Repositories
{
    public class ProductSnapshotRepository : RepositoryBase<ProductSnapshot>, IProductSnapshotRepository
    {
        public ProductSnapshotRepository(ApplicationDbContext context) : base(context) { }

        public async Task AddOrUpdateByName(ProductSnapshot productSnapshot)
        {
            var entityFromDb = await DbSet
                .Where(x => x.Name == productSnapshot.Name)
                .FirstOrDefaultAsync();

            if (entityFromDb is null)
                await Add(productSnapshot);
            else
            {
                entityFromDb.SetValues(productSnapshot.Name, productSnapshot.StockQuantity);
                await Update(entityFromDb);
            }
        }
    }
}
