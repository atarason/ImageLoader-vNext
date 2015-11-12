using Microsoft.Data.Entity.Storage;

namespace ImageLoader.Model.Abstraction
{
    /// <summary>
    ///
    /// </summary>
    public interface IImageLoaderDbContext
    {
        /// <summary>
        ///
        /// </summary>
        Database Database { get; }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        int SaveChanges();
    }
}