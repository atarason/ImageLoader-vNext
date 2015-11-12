using ImageLoader.Models.Entities;
using Microsoft.Data.Entity;

namespace ImageLoader.Models.Contexts
{
    /// <summary>
    ///
    /// </summary>
    public class ImageLoaderDbContext : DbContext
    {
        /// <summary>
        ///
        /// </summary>
        public ImageLoaderDbContext()
        {
            Database.EnsureCreated();
        }

        /// <summary>
        ///
        /// </summary>
        public DbSet<Image> Images { get; set; }
    }
}