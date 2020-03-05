using AutoMapper;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Product;

namespace InventoryManagementSystem.Api.AutoMapperProfiles
{
    public class ServiceProfile : Profile
    {
        public ServiceProfile(){
            CreateMap<Service, ServiceDTO>()
                .BeforeMap((src, dst) => dst.Id = src.Id)
                .BeforeMap((src, dst) => dst.Description = src.Description)
                .BeforeMap((src, dst) => dst.Code = src.Code)
                .BeforeMap((src, dst) => dst.Price = src.Price);
                
            CreateMap<ServiceDTO, Service>()
                .BeforeMap((src, dst) => dst.Id = src.Id)
                .BeforeMap((src, dst) => dst.Description = src.Description)
                .BeforeMap((src, dst) => dst.Code = src.Code)
                .BeforeMap((src, dst) => dst.Price = src.Price);
        }
    }
}