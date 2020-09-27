using Hiper.SynchronizationAPI.Data.Repositories;
using Hiper.SynchronizationAPI.Data.SqlServer.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hiper.SynchronizationAPI.Data.SqlServer.Repositories
{
    public class UnitOfWork : UnitOfWorkBase, IUnitOfWork
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        private IDbContextTransaction transaction = null;

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        #endregion

        #region Transaction

        public void BeginTransaction()
        {
            transaction = _context.Database.BeginTransaction();
        }

        public bool HasCurrentTransaction()
        {
            return _context.Database.CurrentTransaction != null;
        }

        public async Task SaveChanges(CancellationToken cancellationToken = default)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }

        public async Task Commit()
        {
            try
            {
                await SaveChanges();
                transaction.Commit();
            }
            catch (Exception)
            {
                transaction.Rollback();
                throw;
            }
            finally
            {
                transaction = null;
            }
        }

        public void Rollback()
        {
            try
            {
                transaction.Rollback();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }
            finally
            {
                transaction = null;
            }
        }

        public void DetachEntity(object entity) => _context.Entry(entity).State = EntityState.Detached;

        #endregion

        #region Command
        public IProductSnapshotRepository ProductSnapshots => _productSnapshotRepository ??= new ProductSnapshotRepository(_context);
        #endregion

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        private void DisposeRepositories()
        {
            _productSnapshotRepository?.Dispose(); _productSnapshotRepository = null;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DisposeRepositories();
                    transaction?.Dispose();
                    _context?.Dispose();
                }

                disposedValue = true;

            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            // Do not change this code. Put cleanup code in Dispose(bool disposing) above.
            Dispose(true);
        }
        #endregion
    }
}
