using Microsoft.WindowsAzure.Storage.Table;
using System;

namespace ImageLoader.AzureTables
{
    /// <summary>
    ///
    /// </summary>
    public class UserAgent : TableEntity
    {
        /// <summary>
        ///
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        ///
        /// </summary>
        public string Description { get; set; }
    }
}