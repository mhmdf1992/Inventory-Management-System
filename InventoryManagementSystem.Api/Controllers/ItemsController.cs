using System.Collections.Generic;
using AutoMapper;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Product.Tangible;
using InventoryManagementSystem.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InventoryManagementSystem.Api.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        protected readonly EntityService<Item> entityService;
        protected readonly IMapper mapper;
        public ItemsController(EntityService<Item> entityService, IMapper mapper)
        {
            this.entityService = entityService;
            this.mapper = mapper;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ItemDTO>> Get(
            [FromQuery] int skip = 0, [FromQuery] int take = 50){
            var items = entityService.Get(skip, take);

            Response.Headers.Add("X-Pagination", 
                JsonConvert.SerializeObject(new {total = items.Total}));

            return Ok(mapper.Map<IEnumerable<ItemDTO>>(items));
        }

        [HttpGet("find")]
        public ActionResult<IEnumerable<ItemDTO>> FindMatch(
            [FromQuery] string txt,
            [FromQuery] int skip = 0, [FromQuery] int take = 50){
            if(string.IsNullOrEmpty(txt))
                return BadRequest();
            
            var items = entityService.FindMatch(txt, skip, take);
            
            Response.Headers.Add("X-Pagination", 
                JsonConvert.SerializeObject(new {total = items.Total}));

            return Ok(mapper.Map<IEnumerable<ItemDTO>>(items));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<ItemDTO> Get([FromRoute] long? id){
            if(id == null)
                return BadRequest();

            var item = entityService.Get(id.Value);

            if(item == null)
                return NotFound();

            return Ok(mapper.Map<ItemDTO>(item));
        }

        [HttpPost]
        public ActionResult<long> Post([FromBody] ItemDTO itemDto){
            if(! ModelState.IsValid || itemDto == null)
                return BadRequest();
            
            entityService.Insert(mapper.Map<Item>(itemDto)).Save();

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult<long> Put([FromRoute] long? id, [FromBody] ItemDTO itemDto){
            if (! ModelState.IsValid || id == null || itemDto == null )
                return BadRequest();

            var item = entityService.Get(id.Value);
            
            if(item == null)
                return NotFound();

            entityService.Update(item, mapper.Map<Item>(itemDto)).Save();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<long> Delete([FromRoute] long? id){
            if(id == null)
                return BadRequest();

            var item = entityService.Get(id.Value);

            if(item == null)
                return NotFound();

            entityService.Delete(item).Save();

            return Ok();
        }
    }
}