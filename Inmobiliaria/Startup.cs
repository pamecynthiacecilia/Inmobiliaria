using Inmobiliaria.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Threading.Tasks;


namespace Inmobiliaria
{


    //ver using Microsoft.AspNetCore.Http;
    public class Startup
    {
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.

        public void ConfigureServices(IServiceCollection services)
        {

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                      .AddCookie(options =>//el sitio web valida con cookie
                {
                          options.LoginPath = "/Usuarios/Login";
                          options.LogoutPath = "/Usuarios/Logout";
                          options.AccessDeniedPath = "/Home/Restringido";
                      })


                      .AddJwtBearer(options =>//la api web valida con token
                {
                          options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                          {
                              ValidateIssuer = true,
                              ValidateAudience = true,
                              ValidateLifetime = true,
                              ValidateIssuerSigningKey = true,
                              ValidIssuer = configuration["TokenAuthentication:Issuer"],
                              ValidAudience = configuration["TokenAuthentication:Audience"],
                              IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.ASCII.GetBytes(
                                  configuration["TokenAuthentication:SecretKey"])),
                          };
                    // opción extra para usar el token el hub
                    options.Events = new JwtBearerEvents
                          {
                              OnMessageReceived = context =>
                              {
                            // Read the token out of the query string
                            var accessToken = context.Request.Query["access_token"];
                            // If the request is for our hub...
                            var path = context.HttpContext.Request.Path;
                                  if (!string.IsNullOrEmpty(accessToken) &&
                                      path.StartsWithSegments("/chatsegurohub"))
                                  {//reemplazar la url por la usada en la ruta ⬆
                                context.Token = accessToken;
                                  }
                                  return Task.CompletedTask;
                              }
                          };
                      });


            services.AddAuthorization(options =>
            {
                // options.AddPolicy("Empleado", policy => policy.RequireClaim(ClaimTypes.Role, "Administrador", "Empleado"));
                options.AddPolicy("Administrador", policy => policy.RequireRole("Administrador", "SuperAdministrador"));
            });

            services.AddControllersWithViews();

            services.AddDbContext<DataContext>(
                options => options.UseSqlServer(
                    configuration["ConnectionsStrings:DefaultConnection"]));



            services.AddMvc();
            services.AddSignalR();//añade signalR
           /* services.AddSingleton<IUserIdProvider, IUserIdProvider>();*/
            //VER PARA API


            /*    Transient objects are always different; a new instance is provided to every controller and every service.
             Scoped objects are the same within a request, but different across different requests.
             Singleton objects are the same for every object and every request.

             services.AddTransient<IRepositorio<Propietario>, RepositorioPropietario>();
             services.AddTransient<IRepositorioPropietario, RepositorioPropietario>();
             services.AddTransient<IRepositorio<Inquilino>, RepositorioInquilino>();
             services.AddTransient<IRepositorioInmueble, RepositorioInmueble>();
             services.AddTransient<IRepositorioUsuario, RepositorioUsuario>();
             services.AddDbContext<DataContext>(
                 options => options.UseSqlServer(
                     configuration["ConnectionStrings:DefaultConnection"]));
             */



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

            //VER PARA API
              app.UseEndpoints(endpoints =>
                        {
                            endpoints.MapControllerRoute("login", "login/{**accion}", new { controller = "Usuarios", action = "Login" });
                            endpoints.MapControllerRoute("rutaFija", "ruteo/{valor}", new { controller = "Home", action = "Ruta", valor = "defecto" });
                            endpoints.MapControllerRoute("fechas", "{controller=Home}/{action=Fecha}/{anio}/{mes}/{dia}");
                            endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                            
                          /* endpoints.MapHub<ChatHub>("/chathub");//para SignalR
                            endpoints.MapHub<ChatSeguroHub>("/chatsegurohub");//para SignalR*/
                        });

             



        }
    }
}
