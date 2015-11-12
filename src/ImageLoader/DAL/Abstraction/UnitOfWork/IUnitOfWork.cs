using ImageLoader.DAL.Abstraction.Repositories;
using ImageLoader.Models.Entities.BaseEntities;

namespace ImageLoader.DAL.Abstraction.UnitOfWork
{
    /// <summary>
    ///
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : PersistentEntity, new();

        /// <summary>
        ///
        /// </summary>
        IImageRepository ImageRepository { get; }
    }
}