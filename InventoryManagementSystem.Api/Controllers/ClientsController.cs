using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Contact.Client;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InventoryManagementSystem.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;
        public ClientsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ClientDTO>> Get(
            [FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var clients = unitOfWork.ClientRepository.Get(filter: i => !i.IsDeleted);

            Response.Headers.Add("X-Pagination", 
                JsonConvert.SerializeObject(new {total = clients.Count()}));
            return Ok(mapper.Map<IEnumerable<ClientDTO>>(clients.Skip(skip).Take(take)));
        }

        [HttpGet("find")]
        public ActionResult<IEnumerable<ClientDTO>> Find(
            [FromQuery] ClientDTO client,
            [FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var clients = unitOfWork.ClientRepository
                .Get(filter: i => !i.IsDeleted 
                    && (i.Name.Contains(client.Name)
                        || i.Location.Contains(client.Location)
                        || i.Email.Contains(client.Email)
                        || i.Telephone == client.Telephone));

            Response.Headers.Add("X-Pagination", 
                JsonConvert.SerializeObject(new {total = clients.Count()}));
            return Ok(mapper.Map<IEnumerable<ClientDTO>>(clients.Skip(skip).Take(take)));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<ClientDTO> Get([FromRoute] long? id)
        {
            var result = id == null ? null : unitOfWork.ClientRepository.Get(id);
            if(result == null)
                return NotFound();
            return Ok(mapper.Map<ClientDTO>(result));
        }

        [HttpPost]
        public ActionResult<long> Post([FromBody] ClientDTO clientDto)
        {
            if(! ModelState.IsValid || clientDto == null)
                return BadRequest(ModelState);
            
            var client = mapper.Map<Client>(clientDto);

            unitOfWork.ClientRepository.Insert(client);
            unitOfWork.Save();

            return Ok(client.Id);
        }

        [HttpPut("{id}")]
        public ActionResult<long> Put([FromRoute] long? id, [FromBody] ClientDTO clientDto)
        {
            if (! ModelState.IsValid || id == null || clientDto == null )
                return BadRequest(ModelState);

            var clientToUpdate = unitOfWork.ClientRepository.Get(id);
            
            if(clientToUpdate == null)
                return NotFound();
            
            var client = mapper.Map<Client>(clientDto);

            clientToUpdate.Name = client.Name;
            clientToUpdate.Location = client.Location;
            clientToUpdate.Email = client.Email;
            clientToUpdate.Telephone = client.Telephone;

            unitOfWork.ClientRepository.Update(clientToUpdate);
            unitOfWork.Save();

            return Ok(clientToUpdate.Id);
        }

        [HttpDelete("{id}")]
        public ActionResult<long> Delete([FromRoute] long? id)
        {
            var clientToDelete = id == null ? null : unitOfWork.ClientRepository.Get(id);

            if(clientToDelete == null)
                return NotFound();

            unitOfWork.ClientRepository.Delete(clientToDelete);
            unitOfWork.Save();

            return Ok(clientToDelete.Id);
        }
    }
}