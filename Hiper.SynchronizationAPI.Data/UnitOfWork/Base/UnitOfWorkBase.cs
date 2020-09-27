using Hiper.SynchronizationAPI.Data.Repositories;

namespace Hiper.SynchronizationAPI.Data
{
    public abstract class UnitOfWorkBase
    {
        protected IProductSnapshotRepository _productSnapshotRepository;
    }
}
