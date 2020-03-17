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
                .BeforeMap((src, dst) => dst.Id = src.Id)
                .BeforeMap((src, dst) => dst.Email = src.Email)
                .BeforeMap((src, dst) => dst.Firstname = src.Firstname)
                .BeforeMap((src, dst) => dst.Lastname = src.Lastname);

            CreateMap<UserDTO, User>()
                .BeforeMap((src, dst) => dst.Id = src.Id)
                .BeforeMap((src, dst) => dst.Firstname = src.Firstname)
                .BeforeMap((src, dst) => dst.Lastname = src.Lastname)
                .BeforeMap((src, dst) => dst.Email = src.Email)
                .BeforeMap((src, dst) => dst.Password = src.Password);
            
            CreateMap<UserCredentialsDTO, User>()
                .BeforeMap((src, dst) => dst.Email = src.Email)
                .BeforeMap((src, dst) => dst.Password = src.Password);
        }
    }
}