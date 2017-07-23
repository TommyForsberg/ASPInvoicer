using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using InherentInvoiceAlfa.Models.Entities;
using AutoMapper;
using InherentInvoiceAlfa.Models.ViewModels;

namespace InherentInvoiceAlfa
{
    public class Startup
    {
      
        public void ConfigureServices(IServiceCollection services)
        {
            var connstring = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = InherentInvoice; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            var identityConnstring = @"Data Source = (localdb)\MSSQLLocalDB; Initial Catalog = InherentInvoiceIdentity; Integrated Security = True; Connect Timeout = 30; Encrypt = False; TrustServerCertificate = True; ApplicationIntent = ReadWrite; MultiSubnetFailover = False";
            services.AddSession();

            services.AddDbContext<InherentInvoiceContext>(
                options => options.UseSqlServer(connstring));

            services.AddDbContext<IdentityDbContext>(
              options => options.UseSqlServer(identityConnstring));
            services.AddIdentity<IdentityUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Cookies.ApplicationCookie.LoginPath = "/user/SignIn";
            })
               .AddEntityFrameworkStores<IdentityDbContext>()
               .AddDefaultTokenProviders();

            services.AddMvc();

            Mapper.Initialize((config) =>
            {
                config.CreateMap<User, EditUserVM>();
                config.CreateMap<EditUserVM, User>();
                config.CreateMap<CreateCustomerVM, Customer>();
                config.CreateMap<Customer, ListAllCustomersVM>();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            app.UseSession();
            loggerFactory.AddConsole();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseIdentity();
            app.UseMvcWithDefaultRoute();
            app.UseStaticFiles();
        }
    }
}
