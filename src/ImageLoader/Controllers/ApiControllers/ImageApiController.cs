using ImageLoader.DAL.Abstraction.UnitOfWork;
using ImageLoader.Models.Entities;
using ImageLoader.ModelViews;
using ImageLoader.Operations.Abstraction;
using Microsoft.AspNet.Mvc;
using Microsoft.Data.Entity.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImageLoader.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class ImageApiController : Controller
    {
        #region Private Fields

        /// <summary>
        ///
        /// </summary>
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        ///
        /// </summary>
        private readonly IImageOperations _imageOperations;

        #endregion Private Fields

        #region Constructors

        /// <summary>
        ///
        /// </summary>
        /// <param name="unitOfWork"></param>
        /// <param name="imageOperations"></param>
        public ImageApiController(IUnitOfWork unitOfWork, IImageOperations imageOperations)
        {
            _unitOfWork = unitOfWork;
            _imageOperations = imageOperations;
        }

        #endregion Constructors

        #region Public Methods

        // GET: api/values
        [HttpGet]
        public IEnumerable<ImageModelView> Get()
        {
            return _unitOfWork.ImageRepository.GetAll().Select(x => new ImageModelView
            {
                url = x.URL,
                id = x.Id,
                site = x.Site
            }).ToList();
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public Image Get(Guid id)
        {
            return _unitOfWork.ImageRepository.GetById(id);
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ImageModelView model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return HttpBadRequest(ModelState.Values);
                }

                #region Storage Image

                // Donwload Image and storage it to Azure Blob
                var imageBytes = _imageOperations.StorageImage(model.url);

                #endregion Storage Image

                var entity = new Image
                {
                    URL = imageBytes.Item2,
                    Id = Guid.NewGuid(),
                    UpdatedDate = DateTimeOffset.Now,
                    CreatedDate = DateTimeOffset.Now,
                    IsActive = true,
                    Site = model.site
                };

                EntityEntry<Image> result = _unitOfWork.ImageRepository.Insert(entity);
                _unitOfWork.ImageRepository.SubmitChanges();

                model.id = result.Entity.Id;

                return Ok(model);
            }
            catch (Exception e)
            {
                return HttpBadRequest(e.Message);
            }
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(ImageModelView model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return HttpBadRequest(ModelState.Values);
                }

                var entity = _unitOfWork.ImageRepository.GetById(model.id);
                entity.URL = model.url;
                entity.UpdatedDate = DateTimeOffset.Now;

                _unitOfWork.ImageRepository.Edit(entity);
                _unitOfWork.ImageRepository.SubmitChanges();

                return Ok(model);
            }
            catch (Exception e)
            {
                return HttpBadRequest(e.Message);
            }
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return HttpBadRequest(ModelState.Values);
                }

                var image = _unitOfWork.ImageRepository.GetById(id);

                // Remove file from azure blob
                _imageOperations.RemoveImage(image.URL);

                _unitOfWork.ImageRepository.Delete(image);

                return Ok("Image removed successfully");
            }
            catch (Exception e)
            {
                return HttpBadRequest(e.Message);
            }
        }

        #endregion Public Methods
    }
}