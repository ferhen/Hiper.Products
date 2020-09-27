using Hiper.SynchronizationAPI.Data.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hiper.SynchronizationAPI.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        bool HasCurrentTransaction();
        Task SaveChanges(CancellationToken cancellationToken = default);
        Task Commit();
        void Rollback();
        void DetachEntity(object entity);

        IProductSnapshotRepository ProductSnapshots { get; }
    }
}
