using AutoMapper;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Contact.Client;

namespace InventoryManagementSystem.Api.AutoMapperProfiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile(){
            CreateMap<Client, ClientDTO>()
                .BeforeMap((src, dst) => dst.Id = src.Id)
                .BeforeMap((src, dst) => dst.Name = src.Name)
                .BeforeMap((src, dst) => dst.Location = src.Location)
                .BeforeMap((src, dst) => dst.Email = src.Email)
                .BeforeMap((src, dst) => dst.Telephone = src.Telephone);

            CreateMap<ClientDTO, Client>()
                .BeforeMap((src, dst) => dst.Id = src.Id)
                .BeforeMap((src, dst) => dst.Name = src.Name)
                .BeforeMap((src, dst) => dst.Location = src.Location)
                .BeforeMap((src, dst) => dst.Email = src.Email)
                .BeforeMap((src, dst) => dst.Telephone = src.Telephone);
        }
    }
}