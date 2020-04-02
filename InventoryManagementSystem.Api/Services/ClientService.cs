using System.Linq;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.Helpers;
using InventoryManagementSystem.Api.Extensions;
using InventoryManagementSystem.Api.Models.Contact.Client;
namespace InventoryManagementSystem.Api.Services
{
    public class ClientService : EntityService<Client>
    {
        public ClientService(IUnitOfWork unitOfWork) : base(unitOfWork){}

        override
        public PagedList<Client> Get(int skip, int take){
            var clients = unitOfWork.ClientRepository.Get(filter: i => !i.IsDeleted);
            
            return new PagedList<Client>(clients.Skip(skip).Take(take))
                .Set(list => list.Total = clients.Count());
        }

        override
        public PagedList<Client> FindMatch(Client match, int skip, int take){
            var clients = unitOfWork.ClientRepository
                .Get(filter: i => !i.IsDeleted
                    && (i.Name.ToLower().Contains(match.Name.ToLower())
                        || i.Location.ToLower().Contains(match.Location.ToLower())
                        || i.Email.ToLower().Contains(match.Email.ToLower())
                        || i.Telephone.ToLower().Contains(match.Telephone.ToLower())));
            return new PagedList<Client>(clients.Skip(skip).Take(take))
                .Set(list => list.Total = clients.Count());
        }

        override
        public PagedList<Client> FindMatch(string match, int skip, int take){
            var clients = unitOfWork.ClientRepository
                .Get(filter: i => !i.IsDeleted
                    && (i.Name.ToLower().Contains(match.ToLower())
                        || i.Location.ToLower().Contains(match.ToLower())
                        || i.Email.ToLower().Contains(match.ToLower())
                        || i.Telephone.ToLower().Contains(match.ToLower())));
            return new PagedList<Client>(clients.Skip(skip).Take(take))
                .Set(list => list.Total = clients.Count());
        }

        override
        public Client Get(long id){
            return unitOfWork.ClientRepository.Get(id);
        }

        override
        public EntityService<Client> Insert(Client client){
            unitOfWork.ClientRepository.Insert(client);
            return this;
        }

        override
        public EntityService<Client> Update(Client client, Client val){
            unitOfWork.ClientRepository.Update(
                client.Set(s => {
                    s.Name = val.Name;
                    s.Location = val.Location;
                    s.Email = val.Email;
                    s.Telephone = val.Telephone;
                }));
            return this;
        }

        override
        public EntityService<Client> Delete(Client client){
            unitOfWork.ClientRepository.Delete(client);
            return this;
        }
    }
}