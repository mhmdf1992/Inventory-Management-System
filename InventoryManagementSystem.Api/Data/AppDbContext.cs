using InventoryManagementSystem.Api.Models.Contact.Client;
using InventoryManagementSystem.Api.Models.Contact.Supplier;
using InventoryManagementSystem.Api.Models.Product;
using InventoryManagementSystem.Api.Models.Product.Tangible;
using InventoryManagementSystem.Api.Models.User;
using Microsoft.EntityFrameworkCore;

namespace InventoryManagementSystem.Api.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options): base(options){}
        public virtual DbSet<Item> Items {get; set;}
        public virtual DbSet<Service> Services {get; set;}
        public virtual DbSet<Supplier> Suppliers {get; set;}
        public virtual DbSet<Client> Clients {get; set;}
        public virtual DbSet<User> Users {get; set;}
    }
}