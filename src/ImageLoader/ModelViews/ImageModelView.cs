using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.Data.Entity.Metadata;
using Newtonsoft.Json;

namespace ImageLoader.ModelViews
{
    /// <summary>
    ///
    /// </summary>
    public class ImageModelView
    {
        /// <summary>
        ///
        /// </summary>
        [Required]
        [Url]
        [JsonProperty(PropertyName = "url", NullValueHandling = NullValueHandling.Ignore)]
        public string url { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public Guid id { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "isActive", NullValueHandling = NullValueHandling.Ignore)]
        public bool isActive { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "createdDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset createdDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        [JsonProperty(PropertyName = "updateDate", NullValueHandling = NullValueHandling.Ignore)]
        public DateTimeOffset updatedDate { get; set; }

        /// <summary>
        ///
        /// </summary>
        [Required]
        [JsonProperty(PropertyName = "site", NullValueHandling = NullValueHandling.Ignore)]
        public string site { get; set; }
    }
}