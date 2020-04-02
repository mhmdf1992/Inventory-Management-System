using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.Helpers;
using InventoryManagementSystem.Api.Models;

namespace InventoryManagementSystem.Api.Services
{
    public abstract class EntityService<T> where T : IEntity
    {
        protected readonly IUnitOfWork unitOfWork;
        public EntityService(IUnitOfWork unitOfWork){
            this.unitOfWork = unitOfWork;
        }
        public abstract PagedList<T> Get(int skip, int take);
        public abstract PagedList<T> FindMatch(T match, int skip, int take);
        public abstract PagedList<T> FindMatch(string match, int skip, int take);
        public abstract T Get(long id);
        public abstract EntityService<T> Insert(T entity);
        public abstract EntityService<T> Update(T entity, T val);
        public abstract EntityService<T> Delete(T entity);
        public virtual int Save(){
            return this.unitOfWork.Save();
        }
    }
}