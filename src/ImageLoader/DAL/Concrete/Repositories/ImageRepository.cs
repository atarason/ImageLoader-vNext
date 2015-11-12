using ImageLoader.DAL.Abstraction.Repositories;
using ImageLoader.Models.Contexts;
using ImageLoader.Models.Entities;

namespace ImageLoader.DAL.Concrete.Repositories
{
    /// <summary>
    ///
    /// </summary>
    public class ImageRepository : GenericRepository<Image>, IImageRepository
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="dbContext"></param>
        public ImageRepository(ImageLoaderDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}