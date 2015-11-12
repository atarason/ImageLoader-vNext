using Microsoft.Data.Entity.Metadata;
using System;
using System.ComponentModel.DataAnnotations;

namespace ImageLoader.Model.Entities.BaseEntities
{
    /// <summary>
    ///
    /// </summary>
    public abstract class PersistentEntity
    {
        /// <summary>
        /// Id
        /// </summary>
        [Key]
        public virtual Guid Id { get; set; }

        /// <summary>
        /// Is item active
        /// </summary>
        [Required]
        public virtual bool IsActive { get; set; }

        /// <summary>
        /// Created date of item
        /// </summary>
        [Required]
        public virtual DateTimeOffset CreatedDate { get; set; }

        /// <summary>
        /// Updated date of item
        /// </summary>
        [Required]
        public virtual DateTimeOffset UpdatedDate { get; set; }
    }
}