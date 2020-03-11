using System.Collections.Generic;
using AutoMapper;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Product;
using InventoryManagementSystem.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InventoryManagementSystem.Api.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ServicesController : ControllerBase
    {
        protected readonly EntityService<Service> entityService;
        protected readonly IMapper mapper;
        public ServicesController(EntityService<Service> entityService, IMapper mapper)
        {
            this.entityService = entityService;
            this.mapper = mapper;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ServiceDTO>> Get(
            [FromQuery] int skip = 0, [FromQuery] int take = 50){
            var services = entityService.Get(skip, take);

            Response.Headers.Add("X-Pagination", 
                JsonConvert.SerializeObject(new {total = services.Total}));

            return Ok(mapper.Map<IEnumerable<ServiceDTO>>(services));
        }

        [HttpGet("find")]
        public ActionResult<IEnumerable<ServiceDTO>> FindMatch(
            [FromQuery] ServiceDTO service,
            [FromQuery] int skip = 0, [FromQuery] int take = 50){
            if(service == null)
                return BadRequest();
            var services = entityService.FindMatch(mapper.Map<Service>(service), skip, take);

            Response.Headers.Add("X-Pagination", 
                JsonConvert.SerializeObject(new {total = services.Total}));

            return Ok(mapper.Map<IEnumerable<ServiceDTO>>(services));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<ServiceDTO> Get([FromRoute] long? id){
            if(id == null)
                return BadRequest();

            var service = entityService.Get(id.Value);

            if(service == null)
                return NotFound();

            return Ok(mapper.Map<ServiceDTO>(service));
        }

        [HttpPost]
        public ActionResult<long> Post([FromBody] ServiceDTO serviceDto){
            if(! ModelState.IsValid || serviceDto == null)
                return BadRequest();
            
            entityService.Insert(mapper.Map<Service>(serviceDto)).Save();

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult<long> Put([FromRoute] long? id, [FromBody] ServiceDTO serviceDto){
            if (! ModelState.IsValid || id == null || serviceDto == null )
                return BadRequest();

            var service = entityService.Get(id.Value);
            
            if(service == null)
                return NotFound();

            entityService.Update(service, mapper.Map<Service>(serviceDto)).Save();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<long> Delete([FromRoute] long? id){
            if(id == null)
                return BadRequest();

            var service = entityService.Get(id.Value);

            if(service == null)
                return NotFound();

            entityService.Delete(service).Save();

            return Ok();
        }
    }
}