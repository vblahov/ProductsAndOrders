using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using ProductsAndOrders.DataStorage;
using ProductsAndOrders.Services;

namespace ProductsAndOrders
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<IProductService, ProductService>();
            services
                .AddDbContext<LibraryContext>(o =>
                    o.UseSqlServer(Configuration["ConnectionString:StoreDB"]));
            
            
            services.AddSwaggerGen(c => {  
                c.SwaggerDoc("quick-backend", new OpenApiInfo {  
                    Version = "v1",  
                    Title = "Quick Backend",  
                    Description = "This is a test service",  
                    Contact = new OpenApiContact() {  
                        Name = "Blahov Vladyslav", 
                        Email = "hello" 
                    }  
                });  
            });  

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/quick-backend/swagger.json", "My API V1");
            });

            
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}