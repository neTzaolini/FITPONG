using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FIT_PONG.Filters;
using FIT_PONG.Hubs;
using FIT_PONG.Models;
using FIT_PONG.Models.BL;
using FIT_PONG.Services;
using FIT_PONG.Services.Bazni;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ReflectionIT.Mvc.Paging;

namespace FIT_PONG
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
            services.AddControllersWithViews(opcije => {
                var policijarukeuvis = new AuthorizationPolicyBuilder()
                                    .RequireAuthenticatedUser()
                                    .Build();
                opcije.Filters.Add(new AuthorizeFilter(policijarukeuvis));
                opcije.Filters.Add<Filterko>(); // exception filter
            });
            //ovdje registrujemo dbkontekst(ovo je varijanta bez UoW/Repozitorij patterna odnosno bez ikakvih interfejsa
            //u zavisnosti od baze koju zelis koristiti samo mijenjas string ovdje
            //ova funkcija getconnectionstring je kratica za Configuration["ConnectionStrings:imestringa"] samo sto 
            //ona npr ne dozvoljava da ti se json objekat zove drugacije od ConnectionStrings,da sam ga nazvao KonekcijskiStringovi
            //morao bi koristit ovu alternativnu varijatnu //Configuration["KonekcijskiStringovi:Netza"]
            //konkretno ovaj DI kontenjer services se brine oko kreiranja servisa i disposeanja istih zavisno od njihovog vremena
            //trajanja,postoje scoped transient i singleton
            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<InitTakmicenja>();
            services.AddScoped<ELOCalculator>();
            services.AddScoped<Evidentor>();
            services.AddScoped<iEmailServis, FITPONGGmail>();
            services.AddScoped<NotifikacijeHub>();
            services.AddScoped<IGradoviService, GradoviService>();
            services.AddDbContext<MyDb>(opcije => opcije.UseSqlServer(Configuration.GetConnectionString("Plesk")));
            services.AddIdentity<IdentityUser<int>, IdentityRole<int>>(opcije =>
            {
                opcije.Password.RequiredLength = 6;
                opcije.Password.RequireUppercase = false;
                opcije.Password.RequireDigit = false;
                opcije.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<MyDb>()
                .AddDefaultTokenProviders();
            services.AddSignalR();
            services.AddPaging(x =>
            {
                x.ViewName = "Bootstrap4";
                x.HtmlIndicatorDown = " <span>&darr;</span>";
                x.HtmlIndicatorUp= " <span>&uarr;</span>";
            });
			services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>(); //dodano zbog api controllera
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
                //app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
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
                endpoints.MapHub<LampicaHub>("/lampica");
                endpoints.MapHub<ChatHub>("/chathub");
                endpoints.MapHub<NotifikacijeHub>("/notifikacije");
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "defaultWebApi",
                    pattern: "{controller}/{id?}");
            });
        }
    }
}