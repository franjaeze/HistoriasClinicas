using Historias_Clinicas.Data;
using Historias_Clinicas.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Historias_Clinicas
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection builder)
        {
            //services.AddDbContext<HistoriasClinicasContext>(options => options.UseInMemoryDatabase("HistoriaClinicaDb"));
            builder.AddDbContext<HistoriasClinicasContext>(options => options.UseSqlServer(Configuration.GetConnectionString("HistoriasClinicasDBCS")));

            

            builder.PostConfigure<CookieAuthenticationOptions>(IdentityConstants.ApplicationScheme,
                opciones =>
            {
                opciones.LoginPath = "/Account/Iniciarsesion";
                opciones.AccessDeniedPath = "/Account/AccesoDenegado";
                opciones.Cookie.Name = "IdentidadHistoriasClinicasApp";

            });


            builder.AddIdentity<Persona, Rol>().AddEntityFrameworkStores<HistoriasClinicasContext>();
            builder.Configure<IdentityOptions>(opciones =>
            {
                opciones.Password.RequireNonAlphanumeric = false;
                opciones.Password.RequireUppercase = false;
                opciones.Password.RequireLowercase = false;
                opciones.Password.RequireDigit = false;
                opciones.Password.RequiredLength = 5;
            }
            );



            builder.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, HistoriasClinicasContext contexto)
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

                contexto.Database.Migrate();
            
        }
    }
}
