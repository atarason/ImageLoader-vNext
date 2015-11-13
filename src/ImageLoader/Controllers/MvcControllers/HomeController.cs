using System;
using ImageLoader.AzureTables;
using ImageLoader.Operations.Abstraction;
using Microsoft.AspNet.Mvc;

namespace ImageLoader.Controllers
{
    /// <summary>
    ///
    /// </summary>
    public class HomeController : Controller
    {
        /// <summary>
        ///
        /// </summary>
        private readonly IImageOperations _imageOperations;

        /// <summary>
        /// 
        /// </summary>
        public HomeController(IImageOperations imageOperations)
        {
            _imageOperations = imageOperations;
        }

        /// <summary>
        ///
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            var userAgent = Request.Headers["User-Agent"];

            UserAgent model = new UserAgent
            {
                Id = Guid.NewGuid(),
                Description = userAgent[0],
                ETag = Guid.NewGuid().ToString(),
                PartitionKey = Guid.NewGuid().ToString(),
                RowKey = "E",
                Timestamp = DateTimeOffset.Now
            };

            _imageOperations.LogUserAgentData(model);

            return View();
        }
    }
}