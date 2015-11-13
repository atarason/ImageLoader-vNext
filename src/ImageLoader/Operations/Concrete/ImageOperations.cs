using ImageLoader.Operations.Abstraction;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.StorageClient;
using System;
using System.IO;
using System.Net;

namespace ImageLoader.Operations.Concrete
{
    /// <summary>
    ///
    /// </summary>
    public class ImageOperations : IImageOperations
    {
        /// <summary>
        ///
        /// </summary>
        private readonly CloudBlobContainer _container;

        /// <summary>
        /// 
        /// </summary>
        public ImageOperations()
        {
            _container = GetCloudBlobContainerClient();
        }

        #region Public Methods

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public Tuple<string, string> StorageImage(string url)
        {
            using (var webClient = new WebClient())
            {
                using (Stream stream = webClient.OpenRead(url))
                {
                    Tuple<string, string> filePaths = StoreDocumentToBlob(Guid.NewGuid().ToString(), stream, "png");

                    return filePaths;
                }
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="url"></param>
        public void RemoveImage(string url)
        {
            DeleteDocumentFromBlob(url);
        }

        #endregion Public Methods

        #region Private Methods

        private static string CreateDocFullPath(string docName)
        {
            return Path.Combine(Path.GetFileNameWithoutExtension(docName) ?? string.Empty, docName);
        }

        private static string ToUrlSlug(string name)
        {
            return name.Trim('-').ToLower();
        }

        private Tuple<string, string> StoreDocumentToBlob(string path, Stream stream, string type)
        {
            try
            {
                string blobPath = ToUrlSlug(path);

                // Retrieve reference to a blob.
                CloudBlockBlob docBlockBlob = _container.GetBlockBlobReference(blobPath);

                docBlockBlob.Properties.ContentType = "image/png";

                // Create or overwrite the doc with contents from the stream.
                docBlockBlob.UploadFromStream(stream);

                return new Tuple<string, string>(docBlockBlob.Name, docBlockBlob.Uri.ToString());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Deletes document from blob.
        /// </summary>
        /// <param name="blob">Blob name.</param>
        public void DeleteDocumentFromBlob(string blob)
        {
            try
            {
                // Retrieve reference to a blob.
                CloudBlockBlob blockBlob = _container.GetBlockBlobReference(blob);

                // Delete the blob.
                if (!blockBlob.DeleteIfExists())
                {
                    throw new Exception("File doesn't exist in Azure Blob");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        private CloudBlobContainer GetCloudBlobContainerClient()
        {
            try
            {
                // Get Connection string from config.json
                string connectionString = Startup.Configuration["Data:StorageConnectionString"];

                // Retrieve storage account from connection string.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

                // Create the blob client.
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                // Retrieve a reference to a container.
                CloudBlobContainer container = blobClient.GetContainerReference("m4freecontainer");

                // Set permissions.
                container.SetPermissions(new BlobContainerPermissions
                {
                    PublicAccess = BlobContainerPublicAccessType.Container
                });

                // Create the container if it doesn't already exist.
                if (container.CreateIfNotExist())
                {
                }

                return container;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        #endregion Private Methods
    }
}