using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ASPTask5_9.Models;
using ASPTask5_9.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ASPTask5_9
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        private const string userId = "DESKTOP-BSUJI5G\\Dmitry";
        private const string ConnectionString =
            "Server=(localdb)\\MSSQLLocalDB;" +
            "Database=StudentsDb;" +         
            "Trusted_Connection=True;" +
            "MultipleActiveResultSets=true;"+
            "User ID="+userId;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<InfoModel>(Configuration);
            services.AddDbContext<StudentContext>(options => options.UseSqlServer(ConnectionString));
            services.AddTransient<IService, Service>();
            services.AddScoped<Service>();

            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Student}/{action=Index}/{id?}");
            });
        }
    }
}
