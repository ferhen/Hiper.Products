using Hiper.SynchronizationAPI.Core.Models;
using Hiper.SynchronizationAPI.Data.Repositories.Base;
using System.Threading.Tasks;

namespace Hiper.SynchronizationAPI.Data.Repositories
{
    public interface IProductSnapshotRepository : IRepositoryBase<ProductSnapshot>
    {
        Task AddOrUpdateByName(ProductSnapshot product);
    }
}
