using Hiper.SynchronizationAPI.Core.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hiper.SynchronizationAPI.Data.Repositories.Base
{
    public interface IRepositoryBase<TEntity> : IDisposable where TEntity : ModelBase
    {
        Task<TEntity> Add(TEntity entity);
        Task<IEnumerable<TEntity>> AddRange(IEnumerable<TEntity> entities);
        Task<TEntity> Update(TEntity entity);
        Task<IEnumerable<TEntity>> UpdateRange(IEnumerable<TEntity> entities);
        Task<TEntity> AddOrUpdate(TEntity entity);
        Task<IEnumerable<TEntity>> AddOrUpdateRange(IEnumerable<TEntity> entities);
        void Remove(TEntity entity);
        Task Remove(int id);
        void RemoveRange(IEnumerable<TEntity> entities);
        Task RemoveRange(IEnumerable<int> ids);
        Task RemoveRange(IEnumerable<int> ids, int removeBatchSize);
        Task<TEntity> GetById(int id);
        Task<TEntity> GetByIdAsNoTracking(int id);
        Task<IEnumerable<TEntity>> List();
        Task<IEnumerable<TEntity>> ListAsNoTracking();
    }
}
