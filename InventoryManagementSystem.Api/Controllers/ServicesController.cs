using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Product;
using InventoryManagementSystem.Api.Models.Product.Tangible;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InventoryManagementSystem.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;
        public ServicesController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ServiceDTO>> Get(
            [FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var services = unitOfWork.ServiceRepository.Get(filter: i => !i.IsDeleted);

            Response.Headers.Add("X-Pagination", 
                JsonConvert.SerializeObject(new {total = services.Count()}));
            return Ok(mapper.Map<IEnumerable<ServiceDTO>>(services.Skip(skip).Take(take)));
        }

        [HttpGet("find")]
        public ActionResult<IEnumerable<ItemDTO>> Find(
            [FromQuery] ServiceDTO service,
            [FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var services = unitOfWork.ServiceRepository
                .Get(filter: i => !i.IsDeleted 
                    && (i.Code.Contains(service.Code)
                        || i.Description.Contains(service.Description)
                        || i.Price == service.Price));

            Response.Headers.Add("X-Pagination", 
                JsonConvert.SerializeObject(new {total = services.Count()}));
            return Ok(mapper.Map<IEnumerable<ServiceDTO>>(services.Skip(skip).Take(take)));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<ServiceDTO> Get([FromRoute] long? id)
        {
            var result = id == null ? null : unitOfWork.ServiceRepository.Get(id);
            if(result == null)
                return NotFound();
            return Ok(mapper.Map<ServiceDTO>(result));
        }

        [HttpPost]
        public ActionResult<long> Post([FromBody] ServiceDTO serviceDTO)
        {
            if(! ModelState.IsValid || serviceDTO == null)
                return BadRequest(ModelState);
            
            var service = mapper.Map<Service>(serviceDTO);

            unitOfWork.ServiceRepository.Insert(service);
            unitOfWork.Save();

            return Ok(service.Id);
        }

        [HttpPut("{id}")]
        public ActionResult<long> Put([FromRoute] long? id, [FromBody] ServiceDTO serviceDTO)
        {
            if (! ModelState.IsValid || id == null || serviceDTO == null )
                return BadRequest(ModelState);

            var serviceToUpdate = unitOfWork.ServiceRepository.Get(id);
            
            if(serviceToUpdate == null)
                return NotFound();
            
            var service = mapper.Map<Service>(serviceDTO);

            serviceToUpdate.Code = service.Code;
            serviceToUpdate.Price = service.Price;
            serviceToUpdate.Description = service.Description;

            unitOfWork.ServiceRepository.Update(serviceToUpdate);
            unitOfWork.Save();

            return Ok(serviceToUpdate.Id);
        }

        [HttpDelete("{id}")]
        public ActionResult<long> Delete([FromRoute] long? id)
        {
            var serviceToDelete = id == null ? null : unitOfWork.ServiceRepository.Get(id);

            if(serviceToDelete == null)
                return NotFound();

            unitOfWork.ServiceRepository.Delete(serviceToDelete);
            unitOfWork.Save();

            return Ok(serviceToDelete.Id);
        }
    }
}
