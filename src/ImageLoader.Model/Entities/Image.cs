using ImageLoader.Model.Entities.BaseEntities;
using System.ComponentModel.DataAnnotations;

namespace ImageLoader.Model.Entities
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
    }
}