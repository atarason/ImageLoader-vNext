using ImageLoader.Models.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace ImageLoader.Models.Entities
{
    /// <summary>
    ///
    /// </summary>
    public class Image : PersistentEntity
    {
        /// <summary>
        ///
        /// </summary>
        [Required]
        [Url]
        public string URL { get; set; }

        [Required]
        [MaxLength(128)]
        public string Site { get; set; }
    }
}