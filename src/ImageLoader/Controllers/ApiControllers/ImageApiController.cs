using ImageLoader.DAL.Abstraction.UnitOfWork;
using ImageLoader.Models.Entities;
using ImageLoader.ModelViews;
using Microsoft.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Data.Entity.ChangeTracking;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace ImageLoader.Controllers.ApiControllers
{
    [Route("api/[controller]")]
    public class ImageApiController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public ImageApiController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

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
        public async Task<IActionResult> Post([FromBody]ImageModelView model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return HttpBadRequest(ModelState.Values);
                }

                var entity = new Image
                {
                    URL = model.url,
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
                _unitOfWork.ImageRepository.Delete(image);

                return Ok("Image removed successfully");
            }
            catch (Exception e)
            {
                return HttpBadRequest(e.Message);
            }
        }
    }
}