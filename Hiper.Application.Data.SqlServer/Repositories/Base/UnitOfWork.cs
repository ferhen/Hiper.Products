using Hiper.Application.Core.Models;
using Hiper.Application.Data.Repositories;
using Hiper.Application.Data.SqlServer.EventsDispatcher;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Hiper.Application.Data.SqlServer.Repositories.Base
{
    public class UnitOfWork : UnitOfWorkBase, IUnitOfWork
    {
        #region Constructor
        private readonly ApplicationDbContext _context;
        private readonly ApplicationUser _applicationUser;
        private IDbContextTransaction transaction = null;
        private readonly IMediator _mediator;

        public UnitOfWork(ApplicationDbContext context, ApplicationUser applicationUser, IMediator mediator)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _applicationUser = applicationUser ?? throw new ArgumentNullException(nameof(applicationUser));
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
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
            await _mediator.DispatchDomainEventsAsync(_context);
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
        public IProductRepository Products => _productRepository ??= new ProductRepository(_context, _applicationUser);
        public IStockRepository Stocks => _stockRepository ??= new StockRepository(_context, _applicationUser);
        #endregion

        #region IDisposable Support

        private bool disposedValue = false; // To detect redundant calls

        private void DisposeRepositories()
        {
            _productRepository?.Dispose(); _productRepository = null;
            _stockRepository?.Dispose(); _stockRepository = null;
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
