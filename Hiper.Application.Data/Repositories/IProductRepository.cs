using Hiper.Application.Core.Models;
using Hiper.Application.Data.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hiper.Application.Data.Repositories
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        Task<IEnumerable<Product>> ListIncludeStock();
        Task<Product> GetByName(string name);
    }
}
