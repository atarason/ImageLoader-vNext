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
    }
}