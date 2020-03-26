using AutoMapper;
using InventoryManagementSystem.Api.DTOs;
using InventoryManagementSystem.Api.Models.Contact.Client;
using InventoryManagementSystem.Api.Models.User;

namespace InventoryManagementSystem.Api.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile(){
            CreateMap<User, UserDTO>()
                .BeforeMap((src, dst) => dst.id = src.Id)
                .BeforeMap((src, dst) => dst.email = src.Email)
                .BeforeMap((src, dst) => dst.firstname = src.Firstname)
                .BeforeMap((src, dst) => dst.lastname = src.Lastname);

            CreateMap<UserDTO, User>()
                .BeforeMap((src, dst) => dst.Id = src.id)
                .BeforeMap((src, dst) => dst.Firstname = src.firstname)
                .BeforeMap((src, dst) => dst.Lastname = src.lastname)
                .BeforeMap((src, dst) => dst.Email = src.email)
                .BeforeMap((src, dst) => dst.Password = src.password);
            
            CreateMap<UserCredentialsDTO, User>()
                .BeforeMap((src, dst) => dst.Email = src.email)
                .BeforeMap((src, dst) => dst.Password = src.password);
        }
    }
}