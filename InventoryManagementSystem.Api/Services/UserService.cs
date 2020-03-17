using System.Linq;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.Helpers;
using InventoryManagementSystem.Api.Extensions;
using InventoryManagementSystem.Api.Models.User;

namespace InventoryManagementSystem.Api.Services
{
    public class UserService : EntityService<User>, IUserService
    {
        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork){}

        override
        public PagedList<User> Get(int skip, int take){
            var users = unitOfWork.UserRepository.Get(filter: i => !i.IsDeleted);
            
            return new PagedList<User>(users.Skip(skip).Take(take))
                .Set(list => list.Total = users.Count());
        }

        override
        public PagedList<User> FindMatch(User match, int skip, int take){
            var users = unitOfWork.UserRepository
                .Get(filter: i => !i.IsDeleted
                    && i.Email.ToLower().Contains(match.Email.ToLower()));
            return new PagedList<User>(users.Skip(skip).Take(take))
                .Set(list => list.Total = users.Count());
        }

        public User Authenticate(IUserCredentials userCred){
            return unitOfWork.UserRepository
                .Get(filter: i => !i.IsDeleted
                    && i.Email.ToLower().Equals(userCred.Email.ToLower())
                    && i.Password.Equals(userCred.Password)).FirstOrDefault();
        }

        public EntityService<User> Register(User user){
            this.Insert(user);
            return this;
        }

        override
        public User Get(long id){
            return unitOfWork.UserRepository.Get(id);
        }

        override
        public EntityService<User> Insert(User user){
            unitOfWork.UserRepository.Insert(user);
            return this;
        }

        override
        public EntityService<User> Update(User user, User val){
            unitOfWork.UserRepository.Update(
                user.Set(s => {
                    s.Firstname = val.Firstname;
                    s.Lastname = val.Lastname;
                    s.Password = val.Password;
                }));
            return this;
        }

        override
        public EntityService<User> Delete(User user){
            unitOfWork.UserRepository.Delete(user);
            return this;
        }
    }
}