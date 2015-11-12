using ImageLoader.DAL.Abstraction.Repositories;
using ImageLoader.DAL.Abstraction.UnitOfWork;
using ImageLoader.DAL.Concrete.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using ImageLoader.Models.Contexts;
using ImageLoader.Models.Entities.BaseEntities;

namespace ImageLoader.DAL.Concrete.UnitOfWork
{
    public class DBUnitOfWork : IUnitOfWork
    {
        #region Private Fields

        /// <summary>
        ///
        /// </summary>
        private readonly Dictionary<Type, object> repositories_ = new Dictionary<Type, object>();

        /// <summary>
        ///
        /// </summary>
        private readonly ImageLoaderDbContext _dbContext;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="dbContext"></param>
        public DBUnitOfWork(ImageLoaderDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        #endregion Constructors

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <returns></returns>
        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : PersistentEntity, new()
        {
            // Checks if the Dictionary Key contains the Model class
            if (repositories_.Keys.ToList().Contains(typeof(TEntity)))
            {
                // Return the repository for that Model class
                return repositories_[typeof(TEntity)] as IGenericRepository<TEntity>;
            }

            // If the repository for that Model class doesn't exist, create it
            var repository = new GenericRepository<TEntity>(_dbContext);

            // Add it to the dictionary
            repositories_.Add(typeof(TEntity), repository);

            return repository;
        }

        /// <summary>
        ///
        /// </summary>
        public IImageRepository ImageRepository
        {
            get { return new ImageRepository(_dbContext); }
        }

        #endregion Public Methods
    }
}