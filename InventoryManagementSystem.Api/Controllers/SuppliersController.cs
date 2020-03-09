using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Contact.Supplier;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace InventoryManagementSystem.Api.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class SuppliersController : ControllerBase
    {
        protected readonly IUnitOfWork unitOfWork;
        protected readonly IMapper mapper;
        public SuppliersController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<SupplierDTO>> Get(
            [FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var suppliers = unitOfWork.SupplierRepository.Get(filter: i => !i.IsDeleted);

            Response.Headers.Add("X-Pagination", 
                JsonConvert.SerializeObject(new {total = suppliers.Count()}));
            return Ok(mapper.Map<IEnumerable<SupplierDTO>>(suppliers.Skip(skip).Take(take)));
        }

        [HttpGet("find")]
        public ActionResult<IEnumerable<SupplierDTO>> Find(
            [FromQuery] SupplierDTO supplier,
            [FromQuery] int skip = 0, [FromQuery] int take = 50)
        {
            var suppliers = unitOfWork.SupplierRepository
                .Get(filter: i => !i.IsDeleted 
                    && (i.Name.Contains(supplier.Name)
                        || i.Location.Contains(supplier.Location)
                        || i.Email.Contains(supplier.Email)
                        || i.Telephone == supplier.Telephone));

            Response.Headers.Add("X-Pagination", 
                JsonConvert.SerializeObject(new {total = suppliers.Count()}));
            return Ok(mapper.Map<IEnumerable<SupplierDTO>>(suppliers.Skip(skip).Take(take)));
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<SupplierDTO> Get([FromRoute] long? id)
        {
            var result = id == null ? null : unitOfWork.SupplierRepository.Get(id);
            if(result == null)
                return NotFound();
            return Ok(mapper.Map<SupplierDTO>(result));
        }

        [HttpPost]
        public ActionResult<long> Post([FromBody] SupplierDTO supplierDto)
        {
            if(! ModelState.IsValid || supplierDto == null)
                return BadRequest(ModelState);
            
            var supplier = mapper.Map<Supplier>(supplierDto);

            unitOfWork.SupplierRepository.Insert(supplier);
            unitOfWork.Save();

            return Ok(supplier.Id);
        }

        [HttpPut("{id}")]
        public ActionResult<long> Put([FromRoute] long? id, [FromBody] SupplierDTO supplierDto)
        {
            if (! ModelState.IsValid || id == null || supplierDto == null )
                return BadRequest(ModelState);

            var supplierToUpdate = unitOfWork.SupplierRepository.Get(id);
            
            if(supplierToUpdate == null)
                return NotFound();
            
            var supplier = mapper.Map<Supplier>(supplierDto);

            supplierToUpdate.Name = supplier.Name;
            supplierToUpdate.Location = supplier.Location;
            supplierToUpdate.Email = supplier.Email;
            supplierToUpdate.Telephone = supplier.Telephone;

            unitOfWork.SupplierRepository.Update(supplierToUpdate);
            unitOfWork.Save();

            return Ok(supplierToUpdate.Id);
        }

        [HttpDelete("{id}")]
        public ActionResult<long> Delete([FromRoute] long? id)
        {
            var supplierToDelete = id == null ? null : unitOfWork.SupplierRepository.Get(id);

            if(supplierToDelete == null)
                return NotFound();

            unitOfWork.SupplierRepository.Delete(supplierToDelete);
            unitOfWork.Save();

            return Ok(supplierToDelete.Id);
        }
    }
}