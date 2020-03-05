using AutoMapper;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Product.Tangible;

namespace InventoryManagementSystem.Api.AutoMapperProfiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile(){
            CreateMap<Item, ItemDTO>()
                .BeforeMap((src, dst) => dst.Id = src.Id)
                .BeforeMap((src, dst) => dst.Description = src.Description)
                .BeforeMap((src, dst) => dst.Code = src.Code)
                .BeforeMap((src, dst) => dst.Price = src.Price)
                .BeforeMap((src, dst) => dst.ImageBase64 = src.ImageBase64);

            CreateMap<ItemDTO, Item>()
                .BeforeMap((src, dst) => dst.Id = src.Id)
                .BeforeMap((src, dst) => dst.Description = src.Description)
                .BeforeMap((src, dst) => dst.Code = src.Code)
                .BeforeMap((src, dst) => dst.Price = src.Price)
                .BeforeMap((src, dst) => dst.ImageBase64 = src.ImageBase64);
        }
    }
}