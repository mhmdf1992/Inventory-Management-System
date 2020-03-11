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
                    && i.Username.ToLower().Contains(match.Username.ToLower()));
            return new PagedList<User>(users.Skip(skip).Take(take))
                .Set(list => list.Total = users.Count());
        }

        public User Find(User user){
            return unitOfWork.UserRepository
                .Get(filter: i => !i.IsDeleted
                    && i.Username.Equals(user.Username)
                    && i.Password.Equals(user.Password)).FirstOrDefault();
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
                    s.Username = val.Username;
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