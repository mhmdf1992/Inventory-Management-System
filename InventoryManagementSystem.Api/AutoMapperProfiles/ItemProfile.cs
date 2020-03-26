using AutoMapper;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Product.Tangible;

namespace InventoryManagementSystem.Api.AutoMapperProfiles
{
    public class ItemProfile : Profile
    {
        public ItemProfile(){
            CreateMap<Item, ItemDTO>()
                .BeforeMap((src, dst) => dst.id = src.Id)
                .BeforeMap((src, dst) => dst.description = src.Description)
                .BeforeMap((src, dst) => dst.code = src.Code)
                .BeforeMap((src, dst) => dst.price = src.Price)
                .BeforeMap((src, dst) => dst.imageBase64 = src.ImageBase64);

            CreateMap<ItemDTO, Item>()
                .BeforeMap((src, dst) => dst.Id = src.id)
                .BeforeMap((src, dst) => dst.Description = src.description)
                .BeforeMap((src, dst) => dst.Code = src.code)
                .BeforeMap((src, dst) => dst.Price = src.price)
                .BeforeMap((src, dst) => dst.ImageBase64 = src.imageBase64);
        }
    }
}