using Hiper.Application.Core.Models;
using Hiper.Application.Data.Repositories;
using Hiper.Application.Data.SqlServer.Repositories.Base;

namespace Hiper.Application.Data.SqlServer.Repositories
{
    public class StockRepository : RepositoryBase<Stock>, IStockRepository
    {
        public StockRepository(ApplicationDbContext context) : base(context) { }
    }
}
