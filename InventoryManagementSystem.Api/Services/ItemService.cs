using System.Linq;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.Helpers;
using InventoryManagementSystem.Api.Extensions;
using InventoryManagementSystem.Api.Models.Product.Tangible;
using InventoryManagementSystem.Api.Models.Product;

namespace InventoryManagementSystem.Api.Services
{
    public class ItemService : EntityService<Item>, IProductService<Item>
    {
        public ItemService(IUnitOfWork unitOfWork) : base(unitOfWork){}

        override
        public PagedList<Item> Get(int skip, int take){
            var items = unitOfWork.ItemRepository.Get(filter: i => !i.IsDeleted);
            return new PagedList<Item>(items.Skip(skip).Take(take))
                .Set( list => list.Total = items.Count());
        }

        override
        public PagedList<Item> FindMatch(Item match, int skip, int take){
            var items = unitOfWork.ItemRepository
                .Get(filter: i => !i.IsDeleted
                    && (i.Code.ToLower().Contains(match.Code.ToLower())
                        || i.Description.ToLower().Contains(match.Description.ToLower())
                        || i.Price == match.Price));
            return new PagedList<Item>(items.Skip(skip).Take(take))
                .Set(list => list.Total = items.Count());
        }

        override
        public PagedList<Item> FindMatch(string match, int skip, int take){
            var items = unitOfWork.ItemRepository
                .Get(filter: i => !i.IsDeleted
                    && (i.Code.ToLower().Contains(match.ToLower())
                        || i.Description.ToLower().Contains(match.ToLower())));
            return new PagedList<Item>(items.Skip(skip).Take(take))
                .Set(list => list.Total = items.Count());
        }

        override
        public Item Get(long id){
            return unitOfWork.ItemRepository.Get(id);
        }

        override
        public EntityService<Item> Insert(Item item){
            unitOfWork.ItemRepository.Insert(item);
            return this;
        }

        override
        public EntityService<Item> Update(Item item, Item val){
            unitOfWork.ItemRepository.Update(
                item.Set(i => {
                    i.Code = val.Code;
                    i.Description = val.Description;
                    i.Price = val.Price;
                    i.ImageBase64 = val.ImageBase64;
                }));
            return this;
        }

        override
        public EntityService<Item> Delete(Item item){
            unitOfWork.ItemRepository.Delete(item);
            return this;
        }

        public bool Exist(string code){
            return Get(code) != null;
        }

        public Item Get(string code){
            return unitOfWork.ItemRepository.Get(filter: i => i.Code.Equals(code)).FirstOrDefault();
        }
    }
}