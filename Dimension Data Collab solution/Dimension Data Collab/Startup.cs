
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using BackEnd.Models;
using Microsoft.AspNetCore.Identity;
using BackEnd.BussinessLogic;

namespace Dimension_Data_Collab
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
            services.AddAuthentication("CookieAuth").AddCookie("CookieAuth", config =>
            {
                config.Cookie.Name = "CookieAuth";
                config.ExpireTimeSpan = new System.TimeSpan(1, 0, 0);
                config.SlidingExpiration = true;
                config.LoginPath = "/Login";
                config.AccessDeniedPath = "/AccessDenied";
            });

            //my services
            string temp = Configuration["ConnectionString"];
            services.AddSingleton<DataLogic>(new DataLogic(temp, Configuration["DataBase:DataDBCollection"], Configuration["DataBase:DataDBName"]));
            services.AddSingleton<LoginLogic>(new LoginLogic(temp, Configuration["DataBase:LoginDBCollection"], Configuration["DataBase:LoginDBName"]));
            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();
            

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
