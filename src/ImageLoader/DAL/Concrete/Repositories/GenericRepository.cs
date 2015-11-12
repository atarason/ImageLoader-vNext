using ImageLoader.DAL.Abstraction.Repositories;
using System;
using System.Collections.Generic;
using ImageLoader.Models.Contexts;
using ImageLoader.Models.Entities.BaseEntities;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Metadata;
using System.Linq;
using ImageLoader.Models.Entities;
using Microsoft.Data.Entity.ChangeTracking;

namespace ImageLoader.DAL.Concrete.Repositories
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : PersistentEntity, new()
    {
        #region constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="dbContext"></param>
        public GenericRepository(ImageLoaderDbContext dbContext)
        {
            DbContext = dbContext;
        }

        #endregion constructors

        #region operations

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public EntityEntry<TEntity> Insert(TEntity entity)
        {
            return this.DbContext.Set<TEntity>().Add(entity);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Delete(TEntity entity)
        {
            DbContext.Set<TEntity>().Remove(entity);
            DbContext.SaveChanges();

            return entity;
        }

        /// <summary>
        ///
        /// </summary>
        public void SubmitChanges()
        {
            DbContext.SaveChanges();
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetByPage(int pageIndex, int pageSize)
        {
            return DbContext.Set<TEntity>().Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity GetById(Guid id)
        {
            return DbContext.Set<TEntity>().Single(x => x.Id == id).IsActive ? DbContext.Set<TEntity>().Single(x => x.Id == id) : null;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual TEntity Edit(TEntity entity)
        {
            entity.UpdatedDate = DateTimeOffset.Now;

            DbContext.Entry(entity).State = EntityState.Modified;

            return entity;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Deactive(Guid id)
        {
            return ChangeState(id, Models.Enums.EntityState.Deactive);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public TEntity Active(Guid id)
        {
            return ChangeState(id, Models.Enums.EntityState.Active);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public virtual TEntity ChangeState(Guid id, Models.Enums.EntityState state)
        {
            var entity = DbContext.Set<TEntity>().Single(x => x.Id == id);

            entity.UpdatedDate = DateTimeOffset.Now;

            switch (state)
            {
                case Models.Enums.EntityState.Active:
                    entity.IsActive = true;
                    break;

                case Models.Enums.EntityState.Deactive:
                    entity.IsActive = false;
                    break;
            }

            DbContext.SaveChanges();

            return entity;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        public IEnumerable<TEntity> GetAll(bool isActive = true)
        {
            if (isActive)
            {
                return DbContext.Set<TEntity>().Where(x => x.IsActive);
            }

            return DbContext.Set<TEntity>();
        }

        #endregion operations

        #region representations

        /// <summary>
        ///
        /// </summary>
        protected readonly ImageLoaderDbContext DbContext;

        #endregion representations
    }
}