using System.Linq;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.Helpers;
using InventoryManagementSystem.Api.Extensions;
using InventoryManagementSystem.Api.Models.Contact.Supplier;
namespace InventoryManagementSystem.Api.Services
{
    public class SupplierService : EntityService<Supplier>
    {
        public SupplierService(IUnitOfWork unitOfWork) : base(unitOfWork){}

        override
        public PagedList<Supplier> Get(int skip, int take){
            var suppliers = unitOfWork.SupplierRepository.Get(filter: i => !i.IsDeleted);
            return new PagedList<Supplier>(suppliers.Skip(skip).Take(take))
                .Set(list => list.Total = suppliers.Count());
        }

        override
        public PagedList<Supplier> Find(Supplier match, int skip, int take){
            var suppliers = unitOfWork.SupplierRepository
                .Get(filter: i => !i.IsDeleted
                    && (i.Name.ToLower().Contains(match.Name.ToLower())
                        || i.Location.ToLower().Contains(match.Location.ToLower())
                        || i.Email.ToLower().Contains(match.Email.ToLower())
                        || i.Telephone.ToLower().Contains(match.Telephone.ToLower())));
            return new PagedList<Supplier>(suppliers.Skip(skip).Take(take))
                .Set(list => list.Total = suppliers.Count());
        }

        override
        public Supplier Get(long id){
            return unitOfWork.SupplierRepository.Get(id);
        }

        override
        public EntityService<Supplier> Insert(Supplier supplier){
            unitOfWork.SupplierRepository.Insert(supplier);
            return this;
        }

        override
        public EntityService<Supplier> Update(Supplier supplier, Supplier val){
            unitOfWork.SupplierRepository.Update(
                supplier.Set(s => {
                    s.Name = val.Name;
                    s.Location = val.Location;
                    s.Email = val.Email;
                    s.Telephone = val.Telephone;
                }));
            return this;
        }

        override
        public EntityService<Supplier> Delete(Supplier supplier){
            unitOfWork.SupplierRepository.Delete(supplier);
            return this;
        }
    }
}