using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Product.Tangible;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InventoryManagementSystem.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ItemsController : ControllerBase
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;
        public ItemsController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<ItemDTO>> Get(
            [FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var items = unitOfWork.ItemRepository.Get(filter: i => !i.IsDeleted);

            Response.Headers.Add("X-Pagination", 
                JsonConvert.SerializeObject(new {total = items.Count()}));
            return Ok(mapper.Map<IEnumerable<ItemDTO>>(items.Skip(skip).Take(take)));
        }

        [HttpGet("find")]
        public ActionResult<IEnumerable<ItemDTO>> Find(
            [FromQuery] ItemDTO item,
            [FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var items = unitOfWork.ItemRepository
                .Get(filter: i => !i.IsDeleted 
                    && (i.Code.Contains(item.Code)
                        || i.Description.Contains(item.Description)
                        || i.Price == item.Price));

            Response.Headers.Add("X-Pagination", 
                JsonConvert.SerializeObject(new {total = items.Count()}));
            return Ok(mapper.Map<IEnumerable<ItemDTO>>(items.Skip(skip).Take(take)));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<ItemDTO> Get([FromRoute] long? id)
        {
            var result = id == null ? null : unitOfWork.ItemRepository.Get(id);
            if(result == null)
                return NotFound();
            return Ok(mapper.Map<ItemDTO>(result));
        }

        [HttpPost]
        public ActionResult<long> Post([FromBody] ItemDTO itemDto)
        {
            if(! ModelState.IsValid || itemDto == null)
                return BadRequest(ModelState);
            
            var item = mapper.Map<Item>(itemDto);

            unitOfWork.ItemRepository.Insert(item);
            unitOfWork.Save();

            return Ok(item.Id);
        }

        [HttpPut("{id}")]
        public ActionResult<long> Put([FromRoute] long? id, [FromBody] ItemDTO itemDto)
        {
            if (! ModelState.IsValid || id == null || itemDto == null )
                return BadRequest(ModelState);

            var itemToUpdate = unitOfWork.ItemRepository.Get(id);
            
            if(itemToUpdate == null)
                return NotFound();
            
            var item = mapper.Map<Item>(itemDto);

            itemToUpdate.Code = item.Code;
            itemToUpdate.Price = item.Price;
            itemToUpdate.Description = item.Description;
            itemToUpdate.ImageBase64 = item.ImageBase64;

            unitOfWork.ItemRepository.Update(itemToUpdate);
            unitOfWork.Save();

            return Ok(itemToUpdate.Id);
        }

        [HttpDelete("{id}")]
        public ActionResult<long> Delete([FromRoute] long? id)
        {
            var itemToDelete = id == null ? null : unitOfWork.ItemRepository.Get(id);

            if(itemToDelete == null)
                return NotFound();

            unitOfWork.ItemRepository.Delete(itemToDelete);
            unitOfWork.Save();

            return Ok(itemToDelete.Id);
        }
    }
}