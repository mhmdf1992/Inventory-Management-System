using System.Collections.Generic;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.Models.Contact.Client;
using InventoryManagementSystem.Api.Models.Contact.Supplier;
using InventoryManagementSystem.Api.Models.Product;
using InventoryManagementSystem.Api.Models.Product.Tangible;
using InventoryManagementSystem.Api.Models.User;

namespace InventoryManagementSystem.Api.Data
{
    public class EntitiesSeeder : ISeeder
    {
        internal IUnitOfWork unitOfWork;
        public EntitiesSeeder(IUnitOfWork unitOfWork){
            this.unitOfWork = unitOfWork;
        }
        public void Seed()
        {
            if(! unitOfWork.ItemRepository.Any())
            {
                List<Item> seeds = new List<Item>(){
                    new Item(){Id = 1, Description = "snips 25g", Code = "0001", Price = 0.5},
                    new Item(){Id = 2, Description = "snips 50g", Code = "0002", Price = 1},
                    new Item(){Id = 3, Description = "snips 100g", Code = "0003", Price = 1.5},
                    new Item(){Id = 4, Description = "snips 125g", Code = "0004", Price = 2},
                    new Item(){Id = 5, Description = "snips 150g", Code = "0005", Price = 3},
                    new Item(){Id = 6, Description = "snips 25g", Code = "0006", Price = 0.5},
                    new Item(){Id = 7, Description = "snips 50g", Code = "0007", Price = 1},
                    new Item(){Id = 8, Description = "snips 100g", Code = "0008", Price = 1.5},
                    new Item(){Id = 9, Description = "snips 125g", Code = "0009", Price = 2},
                    new Item(){Id = 10, Description = "snips 150g", Code = "0010", Price = 3},
                    new Item(){Id = 11, Description = "snips 25g", Code = "0011", Price = 0.5},
                    new Item(){Id = 12, Description = "snips 50g", Code = "0012", Price = 1},
                    new Item(){Id = 13, Description = "snips 100g", Code = "0013", Price = 1.5},
                    new Item(){Id = 14, Description = "snips 125g", Code = "0014", Price = 2},
                    new Item(){Id = 15, Description = "snips 150g", Code = "0015", Price = 3},
                    new Item(){Id = 16, Description = "snips 25g", Code = "0016", Price = 0.5},
                    new Item(){Id = 17, Description = "snips 50g", Code = "0017", Price = 1},
                    new Item(){Id = 18, Description = "snips 100g", Code = "0018", Price = 1.5},
                    new Item(){Id = 19, Description = "snips 125g", Code = "0019", Price = 2},
                    new Item(){Id = 20, Description = "snips 150g", Code = "0020", Price = 3},
                    new Item(){Id = 21, Description = "snips 25g", Code = "0021", Price = 0.5},
                    new Item(){Id = 22, Description = "snips 50g", Code = "0022", Price = 1},
                    new Item(){Id = 23, Description = "snips 100g", Code = "0023", Price = 1.5},
                    new Item(){Id = 24, Description = "snips 125g", Code = "0024", Price = 2},
                    new Item(){Id = 25, Description = "snips 150g", Code = "0025", Price = 3}
                };
                seeds.ForEach(item => unitOfWork.ItemRepository.Insert(item));
            }

            if(! unitOfWork.ServiceRepository.Any())
            {
                List<Service> seeds = new List<Service>(){
                    new Service(){Id = 1, Description = "extract container", Code = "s0001", Price = 100},
                    new Service(){Id = 2, Description = "install house furniture", Code = "s0002", Price = 150},
                    new Service(){Id = 3, Description = "business consultancy", Code = "s0003", Price = 1.5},
                    new Service(){Id = 4, Description = "legal advice", Code = "s0004", Price = 2},
                    new Service(){Id = 5, Description = "extract container", Code = "s0005", Price = 100},
                    new Service(){Id = 6, Description = "install house furniture", Code = "s0006", Price = 150},
                    new Service(){Id = 7, Description = "business consultancy", Code = "s0007", Price = 1.5},
                    new Service(){Id = 8, Description = "legal advice", Code = "s0008", Price = 2},
                    new Service(){Id = 9, Description = "extract container", Code = "s0009", Price = 100},
                    new Service(){Id = 10, Description = "install house furniture", Code = "s0010", Price = 150},
                    new Service(){Id = 11, Description = "business consultancy", Code = "s0011", Price = 1.5},
                    new Service(){Id = 12, Description = "legal advice", Code = "s0012", Price = 2},
                    new Service(){Id = 13, Description = "extract container", Code = "s0013", Price = 100},
                    new Service(){Id = 14, Description = "install house furniture", Code = "s0014", Price = 150},
                    new Service(){Id = 15, Description = "business consultancy", Code = "s0015", Price = 1.5},
                    new Service(){Id = 16, Description = "legal advice", Code = "s0016", Price = 2},
                    new Service(){Id = 17, Description = "business consultancy", Code = "s0017", Price = 1},
                    new Service(){Id = 18, Description = "extract container", Code = "s0018", Price = 100},
                    new Service(){Id = 19, Description = "install house furniture", Code = "s0019", Price = 150},
                    new Service(){Id = 20, Description = "business consultancy", Code = "s0020", Price = 1.5},
                    new Service(){Id = 21, Description = "legal advice", Code = "s0021", Price = 2},
                    new Service(){Id = 22, Description = "extract container", Code = "s0022", Price = 100},
                    new Service(){Id = 23, Description = "install house furniture", Code = "s0023", Price = 150},
                    new Service(){Id = 24, Description = "business consultancy", Code = "s0024", Price = 1.5},
                    new Service(){Id = 25, Description = "legal advice", Code = "s00025", Price = 2},
                };
                seeds.ForEach(service => unitOfWork.ServiceRepository.Insert(service));
            }
            
            if(! unitOfWork.SupplierRepository.Any()){
                List<Supplier> seeds = new List<Supplier>(){
                    new Supplier() {Id = 1, Name = "Chopachops", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"},
                    new Supplier() {Id = 2, Name = "Mobili Cruz", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"},
                    new Supplier() {Id = 3, Name = "Silva Group", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"}
                };
                seeds.ForEach(s => unitOfWork.SupplierRepository.Insert(s));
            }

            if(! unitOfWork.ClientRepository.Any()){
                List<Client> seeds = new List<Client>(){
                    new Client() {Id = 1, Name = "Marco Verati", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"},
                    new Client() {Id = 2, Name = "Benjamin Stone", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"},
                    new Client() {Id = 3, Name = "Bernardo Silva", Telephone = "961 3 822 106", Location = "Green fields - wall street 101 GF"}
                };
                seeds.ForEach(s => unitOfWork.ClientRepository.Insert(s));
            }

            if(! unitOfWork.UserRepository.Any()){
                List<User> seeds = new List<User>(){
                    new User() {Id = 1, Firstname="Mhmd", Lastname="Fayad", Email = "mhmdfayad@gmail.com", Password="admin"},
                    new User() {Id = 2, Firstname="Jackie", Lastname="Chan", Email = "jackie_chan@gmail.com", Password="102030"},
                    new User() {Id = 3, Firstname="Thomas", Lastname="Party", Email = "thomasp1919@gmail.com", Password="405060"}
                };
                seeds.ForEach(s => unitOfWork.UserRepository.Insert(s));
            }
            
            unitOfWork.Save();
        }
    }
}