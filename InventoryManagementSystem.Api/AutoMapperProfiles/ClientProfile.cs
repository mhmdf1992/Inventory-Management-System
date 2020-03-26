using AutoMapper;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Contact.Client;

namespace InventoryManagementSystem.Api.AutoMapperProfiles
{
    public class ClientProfile : Profile
    {
        public ClientProfile(){
            CreateMap<Client, ClientDTO>()
                .BeforeMap((src, dst) => dst.id = src.Id)
                .BeforeMap((src, dst) => dst.name = src.Name)
                .BeforeMap((src, dst) => dst.location = src.Location)
                .BeforeMap((src, dst) => dst.email = src.Email)
                .BeforeMap((src, dst) => dst.telephone = src.Telephone);

            CreateMap<ClientDTO, Client>()
                .BeforeMap((src, dst) => dst.Id = src.id)
                .BeforeMap((src, dst) => dst.Name = src.name)
                .BeforeMap((src, dst) => dst.Location = src.location)
                .BeforeMap((src, dst) => dst.Email = src.email)
                .BeforeMap((src, dst) => dst.Telephone = src.telephone);
        }
    }
}