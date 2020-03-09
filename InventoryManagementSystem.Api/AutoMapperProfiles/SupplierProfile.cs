using AutoMapper;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Contact.Supplier;

namespace InventoryManagementSystem.Api.AutoMapperProfiles
{
    public class SupplierProfile : Profile
    {
        public SupplierProfile(){
            CreateMap<Supplier, SupplierDTO>()
                .BeforeMap((src, dst) => dst.Id = src.Id)
                .BeforeMap((src, dst) => dst.Name = src.Name)
                .BeforeMap((src, dst) => dst.Location = src.Location)
                .BeforeMap((src, dst) => dst.Email = src.Email)
                .BeforeMap((src, dst) => dst.Telephone = src.Telephone);

            CreateMap<SupplierDTO, Supplier>()
                .BeforeMap((src, dst) => dst.Id = src.Id)
                .BeforeMap((src, dst) => dst.Name = src.Name)
                .BeforeMap((src, dst) => dst.Location = src.Location)
                .BeforeMap((src, dst) => dst.Email = src.Email)
                .BeforeMap((src, dst) => dst.Telephone = src.Telephone);
        }
    }
}