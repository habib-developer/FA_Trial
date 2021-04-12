using AutoMapper.EquivalencyExpression;
using FA.Core.Domain;
using FA.Core.Infrastructure;
using FA.Core.Services;
using FA.Data;
using FA.Services;
using FA.Web.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FA.Web
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
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //TODO:Register app services
            services.AddTransient<IAsyncRepository<Project>, EfAsyncRepository<Project>>();
            services.AddTransient<IAsyncRepository<Freelancer>, EfAsyncRepository<Freelancer>>();
            services.AddTransient<IAsyncRepository<Availability>, EfAsyncRepository<Availability>>();
            services.AddTransient<IProjectService, ProjectService>();
            services.AddTransient<IFreelancerService, FreelancerService>();
            services.AddTransient<IAvailabilityService, AvailabilityService>();
            
            //TODO:Setup Automapper
            services.AddAutoMapper(e =>
            {
                e.AddProfile(new AutomappingProfile());
                e.AddCollectionMappers();
            });
            services.AddControllersWithViews()
                .AddNewtonsoftJson(options=> {
                    options.SerializerSettings.ContractResolver = new DefaultContractResolver();
                });
            services.AddAuthentication().AddMicrosoftAccount(microsoftOptions =>
            {
                microsoftOptions.ClientId = Configuration["Authentication:Microsoft:ClientId"];
                microsoftOptions.ClientSecret = Configuration["Authentication:Microsoft:ClientSecret"];
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            //Its important the rewind us added before UseMvc
            app.Use(next => context => { context.Request.EnableBuffering(); return next(context); });
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
                endpoints.MapRazorPages();
            });
        }
    }
}
