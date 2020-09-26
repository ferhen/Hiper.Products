using Hiper.Application.Data.Repositories;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hiper.Application.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void BeginTransaction();
        bool HasCurrentTransaction();
        Task SaveChanges(CancellationToken cancellationToken = default);
        Task Commit();
        void Rollback();
        void DetachEntity(object entity);

        IProductRepository Products { get; }
        IStockRepository Stocks { get; }
    }
}
