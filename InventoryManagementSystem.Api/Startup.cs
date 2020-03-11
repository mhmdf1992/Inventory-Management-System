using System;
using System.Text;
using AutoMapper;
using InventoryManagementSystem.Api.DAL.Repository;
using InventoryManagementSystem.Api.DAL.UnitOfWork;
using InventoryManagementSystem.Api.Data;
using InventoryManagementSystem.Api.Models.Contact.Client;
using InventoryManagementSystem.Api.Models.Contact.Supplier;
using InventoryManagementSystem.Api.Models.Product;
using InventoryManagementSystem.Api.Models.Product.Tangible;
using InventoryManagementSystem.Api.Models.User;
using InventoryManagementSystem.Api.Services;
using InventoryManagementSystem.Api.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;

namespace InventoryManagementSystem.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(options => options
                .UseSqlite(Configuration.GetConnectionString("DefaultConnection")))
                .AddAutoMapper(typeof(Startup))
                .AddScoped<IRepository<Item>, EntityRepository<Item>>()
                .AddScoped<IRepository<Service>, EntityRepository<Service>>()
                .AddScoped<IRepository<Supplier>, EntityRepository<Supplier>>()
                .AddScoped<IRepository<Client>, EntityRepository<Client>>()
                .AddScoped<IRepository<User>, EntityRepository<User>>()
                .AddScoped<IUnitOfWork, EntitiesUnitOfWork>()
                .AddScoped<ISeeder, EntitiesSeeder>()
                .AddScoped<EntityService<Item>, ItemService>()
                .AddScoped<EntityService<Service>, ServiceService>()
                .AddScoped<EntityService<Supplier>, SupplierService>()
                .AddScoped<EntityService<Client>, ClientService>()
                .AddScoped<EntityService<User>, UserService>()
                .AddScoped<IUserService>(x => new UserService(x.GetRequiredService<IUnitOfWork>()))
                .AddScoped<IAuthService>(x => new JWTAuthService(
                    x.GetRequiredService<IUserService>(),
                    Configuration.GetValue<string>("TokenKey"),
                    DateTime.UtcNow.AddHours(1)))
                .AddCors(options =>{
                    options.AddDefaultPolicy(builder =>{
                        builder.WithOrigins("http://localhost:3000").AllowAnyHeader().AllowAnyMethod()
                            .WithExposedHeaders("X-Pagination");
                    });
                })
                .AddAuthentication(x =>{
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                    .AddJwtBearer(x =>{
                        x.RequireHttpsMetadata = false;
                        x.SaveToken = true;
                        x.TokenValidationParameters = new TokenValidationParameters{   
                            ValidateIssuerSigningKey = true,
                            IssuerSigningKey = new SymmetricSecurityKey(
                                Encoding.ASCII.GetBytes(Configuration.GetValue<string>("TokenKey"))),
                            ValidateIssuer = false,
                            ValidateAudience = false
                            };
                    });

                services.AddControllers().AddNewtonsoftJson();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env,
             AppDbContext dbContext, ISeeder seeder)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication()
                .UseAuthorization()
                .UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod())
                .UseHttpsRedirection()
                .UseRouting()
                .UseCors()
                .UseAuthorization()
                .UseEndpoints(endpoints =>{
                    endpoints.MapControllers();
                });

            dbContext.Database.Migrate();
            seeder.Seed();
        }
    }
}
