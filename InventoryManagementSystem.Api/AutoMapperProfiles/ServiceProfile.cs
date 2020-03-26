using AutoMapper;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Product;

namespace InventoryManagementSystem.Api.AutoMapperProfiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile(){
            CreateMap<Service, ServiceDTO>()
                .BeforeMap((src, dst) => dst.id = src.Id)
                .BeforeMap((src, dst) => dst.description = src.Description)
                .BeforeMap((src, dst) => dst.code = src.Code)
                .BeforeMap((src, dst) => dst.price = src.Price);
                
            CreateMap<ServiceDTO, Service>()
                .BeforeMap((src, dst) => dst.Id = src.id)
                .BeforeMap((src, dst) => dst.Description = src.description)
                .BeforeMap((src, dst) => dst.Code = src.code)
                .BeforeMap((src, dst) => dst.Price = src.price);
        }
    }
}