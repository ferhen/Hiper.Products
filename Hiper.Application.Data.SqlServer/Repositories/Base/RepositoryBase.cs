using Hiper.Application.Core.Models;
using Hiper.Application.Data.Repositories.Base;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace Hiper.Application.Data.SqlServer.Repositories.Base
{
    public abstract class RepositoryBase<TModel> : IRepositoryBase<TModel> where TModel : ModelBase
    {
        #region Constructor
        protected ApplicationDbContext Db;
        protected DbSet<TModel> DbSet;
        protected readonly string CurrentUser;

        public RepositoryBase(ApplicationDbContext context)
        {
            Db = context;
            DbSet = Db.Set<TModel>();
            CurrentUser = context.CurrentUser;
        }
        #endregion

        public virtual async Task<TModel> Add(TModel entity)
        {
            entity.CreatedOn = entity.LastUpdateOn = DateTimeOffset.Now;
            entity.CreatedBy = entity.LastUpdatedBy = CurrentUser;
            await DbSet.AddAsync(entity);
            return await GetById(entity.Id);
        }

        public virtual async Task<IEnumerable<TModel>> AddRange(IEnumerable<TModel> entities)
        {
            foreach (var entity in entities)
            {
                entity.CreatedOn = entity.LastUpdateOn = DateTimeOffset.Now;
                entity.CreatedBy = entity.LastUpdatedBy = CurrentUser;
            }
            await DbSet.AddRangeAsync(entities);
            return entities;
        }

        public virtual async Task<TModel> Update(TModel entity)
        {
            entity.LastUpdateOn = DateTimeOffset.Now;
            entity.LastUpdatedBy = CurrentUser;
            var entry = Db.Entry(entity);
            entry.State = EntityState.Modified;
            entry.Property(e => e.CreatedOn).IsModified = false;
            entry.Property(e => e.CreatedBy).IsModified = false;
            return await GetById(entity.Id);
        }

        public virtual async Task<IEnumerable<TModel>> UpdateRange(IEnumerable<TModel> entities)
        {
            var entitiesUpdated = new List<TModel>();
            foreach (var entity in entities)
                entitiesUpdated.Add(await Update(entity));
            return entitiesUpdated;
        }

        public virtual async Task<TModel> AddOrUpdate(TModel entity)
        {
            if (entity.Id == 0)
                return await Add(entity);
            else
                return await Update(entity);
        }

        public virtual async Task<IEnumerable<TModel>> AddOrUpdateRange(IEnumerable<TModel> entities)
        {
            foreach (var entity in entities)
                if (entity.Id == 0)
                    await Add(entity);
                else
                    await Update(entity);
            return entities;
        }

        public virtual void Remove(TModel entity)
        {
            Db.Entry(entity).State = EntityState.Deleted;
        }

        public virtual async Task Remove(int id)
        {
            var entity = await DbSet.FindAsync(id);
            Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TModel> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public virtual async Task RemoveRange(IEnumerable<int> ids, int removeBatchSize) => await DbSet
                .Where(x => ids.Contains(x.Id))
                .DeleteAsync(x => x.BatchSize = removeBatchSize);

        public virtual async Task RemoveRange(IEnumerable<int> ids)
        {
            foreach (var id in ids)
                await Remove(id);

        }

        public virtual async Task<TModel> GetById(int id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<TModel> GetByIdAsNoTracking(int id)
        {
            TModel entity = await DbSet.FindAsync(id);
            Db.Entry(entity).State = EntityState.Detached;
            return entity;
        }

        public virtual async Task<IEnumerable<TModel>> List() => await DbSet.ToListAsync();

        public virtual async Task<IEnumerable<TModel>> ListAsNoTracking() => await DbSet.AsNoTracking().ToListAsync();

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // dispose managed state (managed objects).
                }

                // free unmanaged resources (unmanaged objects) and override a finalizer below.
                // set large fields to null.

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
