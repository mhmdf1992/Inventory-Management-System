using System.Collections.Generic;
using AutoMapper;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Contact.Client;
using InventoryManagementSystem.Api.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InventoryManagementSystem.Api.Controllers
{
    [Authorize]
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        protected readonly EntityService<Client> entityService;
        protected readonly IMapper mapper;
        public ClientsController(EntityService<Client> entityService, IMapper mapper)
        {
            this.entityService = entityService;
            this.mapper = mapper;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ClientDTO>> Get(
            [FromQuery] int skip = 0, [FromQuery] int take = 50){
            var clients = entityService.Get(skip, take);
            
            Response.Headers.Add("X-Pagination", 
                JsonConvert.SerializeObject(new {total = clients.Total}));

            return Ok(mapper.Map<IEnumerable<ClientDTO>>(clients));
        }

        [HttpGet("find")]
        public ActionResult<IEnumerable<ClientDTO>> FindMatch(
            [FromQuery] ClientDTO client,
            [FromQuery] int skip = 0, [FromQuery] int take = 50){
            if(client == null)
                return BadRequest();
                
            var clients = entityService.FindMatch(mapper.Map<Client>(client), skip, take);

            Response.Headers.Add("X-Pagination", 
                JsonConvert.SerializeObject(new {total = clients.Total}));

            return Ok(mapper.Map<IEnumerable<ClientDTO>>(clients));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<ClientDTO> Get([FromRoute] long? id){
            if(id == null)
                return BadRequest();

            var client = entityService.Get(id.Value);

            if(client == null)
                return NotFound();

            return Ok(mapper.Map<ClientDTO>(client));
        }

        [HttpPost]
        public ActionResult<long> Post([FromBody] ClientDTO clientDto){
            if(! ModelState.IsValid || clientDto == null)
                return BadRequest();
            
            entityService.Insert(mapper.Map<Client>(clientDto)).Save();

            return Ok();
        }

        [HttpPut("{id}")]
        public ActionResult<long> Put([FromRoute] long? id, [FromBody] ClientDTO clientDto){
            if (! ModelState.IsValid || id == null || clientDto == null )
                return BadRequest();

            var client = entityService.Get(id.Value);
            
            if(client == null)
                return NotFound();

            entityService.Update(client, mapper.Map<Client>(clientDto)).Save();

            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult<long> Delete([FromRoute] long? id){
            if(id == null)
                return BadRequest();

            var client = entityService.Get(id.Value);

            if(client == null)
                return NotFound();

            entityService.Delete(client).Save();

            return Ok();
        }
    }
}