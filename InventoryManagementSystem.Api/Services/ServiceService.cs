using System.Linq;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.Helpers;
using InventoryManagementSystem.Api.Extensions;
using InventoryManagementSystem.Api.Models.Product;

namespace InventoryManagementSystem.Api.Services
{
    public class ServiceService : EntityService<Service>, IProductService<Service>
    {
        public ServiceService(IUnitOfWork unitOfWork) : base(unitOfWork){}

        override
        public PagedList<Service> Get(int skip, int take){
            var services = unitOfWork.ServiceRepository.Get(filter: i => !i.IsDeleted);
            return new PagedList<Service>(services.Skip(skip).Take(take))
                .Set(list => list.Total = services.Count());
        }

        override
        public PagedList<Service> FindMatch(Service match, int skip, int take){
            var services = unitOfWork.ServiceRepository
                .Get(filter: i => !i.IsDeleted
                    && (i.Code.ToLower().Contains(match.Code.ToLower())
                        || i.Description.ToLower().Contains(match.Description.ToLower())
                        || i.Price == match.Price));
            return new PagedList<Service>(services.Skip(skip).Take(take))
                .Set(list => list.Total = services.Count());
        }

        override
        public PagedList<Service> FindMatch(string match, int skip, int take){
            var services = unitOfWork.ServiceRepository
                .Get(filter: i => !i.IsDeleted
                    && (i.Code.ToLower().Contains(match.ToLower())
                        || i.Description.ToLower().Contains(match.ToLower())));
            return new PagedList<Service>(services.Skip(skip).Take(take))
                .Set(list => list.Total = services.Count());
        }

        override
        public Service Get(long id){
            return unitOfWork.ServiceRepository.Get(id);
        }

        override
        public EntityService<Service> Insert(Service service){
            unitOfWork.ServiceRepository.Insert(service);
            return this;
        }

        override
        public EntityService<Service> Update(Service service, Service val){
            unitOfWork.ServiceRepository.Update(
                service.Set(s => {
                    s.Code = val.Code;
                    s.Description = val.Description;
                    s.Price = val.Price;
                }));
            return this;
        }

        override
        public EntityService<Service> Delete(Service service){
            unitOfWork.ServiceRepository.Delete(service);
            return this;
        }

        public bool Exist(string code)
        {
            return Get(code) != null;
        }

        public Service Get(string code){
            return unitOfWork.ServiceRepository.Get(filter: i => i.Code.Equals(code)).FirstOrDefault();
        }
    }
}