using ImageLoader.Model.Entities;
using Microsoft.Data.Entity;

namespace ImageLoader.Model.Contexts
{
    /// <summary>
    ///
    /// </summary>
    public class ImageLoaderDbContext : DbContext
    {
        /// <summary>
        ///
        /// </summary>
        public DbSet<Image> Images { get; set; }
    }
}