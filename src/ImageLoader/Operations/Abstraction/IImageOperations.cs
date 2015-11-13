using ImageLoader.AzureTables;
using System;

namespace ImageLoader.Operations.Abstraction
{
    /// <summary>
    ///
    /// </summary>
    public interface IImageOperations
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        Tuple<string, string> StorageImage(string url);

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        void RemoveImage(string url);

        /// <summary>
        ///
        /// </summary>
        /// <param name="model"></param>
        void LogUserAgentData(UserAgent model);
    }
}