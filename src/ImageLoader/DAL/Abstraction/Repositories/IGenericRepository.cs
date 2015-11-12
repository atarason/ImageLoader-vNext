using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.ChangeTracking;

namespace ImageLoader.DAL.Abstraction.Repositories
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IGenericRepository<TEntity> where TEntity : class, new()
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="isActive"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetAll(bool isActive = true);

        /// <summary>
        ///
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        IEnumerable<TEntity> GetByPage(int pageIndex, int pageSize);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity GetById(Guid id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Edit(TEntity entity);

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> Insert(TEntity entity);

        /// <summary>
        ///
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        TEntity Delete(TEntity entity);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Deactive(Guid id);

        /// <summary>
        ///
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        TEntity Active(Guid id);

        /// <summary>
        ///
        /// </summary>
        void SubmitChanges();
    }
}