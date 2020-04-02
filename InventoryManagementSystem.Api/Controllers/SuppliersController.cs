using System.Collections.Generic;
using AutoMapper;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Contact.Supplier;
using InventoryManagementSystem.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InventoryManagementSystem.Api.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        protected readonly EntityService<Supplier> entityService;
        protected readonly IMapper mapper;
        public SuppliersController(EntityService<Supplier> entityService, IMapper mapper)
        {
            this.entityService = entityService;
            this.mapper = mapper;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<SupplierDTO>> Get(
            [FromQuery] int skip = 0, [FromQuery] int take = 50){
            var suppliers = entityService.Get(skip, take);

            Response.Headers.Add("X-Pagination", 
                JsonConvert.SerializeObject(new {total = suppliers.Total}));

            return Ok(mapper.Map<IEnumerable<SupplierDTO>>(suppliers));
        }

        [HttpGet("find")]
        public ActionResult<IEnumerable<SupplierDTO>> FindMatch(
            [FromQuery] string txt,
            [FromQuery] int skip = 0, [FromQuery] int take = 50){
            if(string.IsNullOrEmpty(txt))
                return BadRequest();

            var suppliers = entityService.FindMatch(txt, skip, take);
            
            Response.Headers.Add("X-Pagination", 
                JsonConvert.SerializeObject(new {total = suppliers.Total}));

            return Ok(mapper.Map<IEnumerable<SupplierDTO>>(suppliers));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<SupplierDTO> Get([FromRoute] long? id){
            if(id == null)
                return BadRequest();

            var supplier = entityService.Get(id.Value);

            if(supplier == null)
                return NotFound();

            return Ok(mapper.Map<SupplierDTO>(supplier));
        }

        [HttpPost]
        public ActionResult<long> Post([FromBody] SupplierDTO supplierDto){
            if(! ModelState.IsValid || supplierDto == null)
                return BadRequest();
            
            entityService.Insert(mapper.Map<Supplier>(supplierDto)).Save();

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult<long> Put([FromRoute] long? id, [FromBody] SupplierDTO supplierDto){
            if (! ModelState.IsValid || id == null || supplierDto == null )
                return BadRequest();

            var supplier = entityService.Get(id.Value);
            
            if(supplier == null)
                return NotFound();

            entityService.Update(supplier, mapper.Map<Supplier>(supplierDto)).Save();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<long> Delete([FromRoute] long? id){
            if(id == null)
                return BadRequest();

            var supplier = entityService.Get(id.Value);

            if(supplier == null)
                return NotFound();

            entityService.Delete(supplier).Save();

            return Ok();
        }
    }
}