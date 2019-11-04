using System;
using System.Collections.Generic;
using System.Security.Claims;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Data;
using Cynosura.Template.Web.Infrastructure;
using Cynosura.Web;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Cynosura.Template.Web
{
    public class Startup
    {
        private readonly IWebHostEnvironment _hostingEnvironment;

        public Startup(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;

            var configurationBuilder = new ConfigurationBuilder();
            configurationBuilder.SetBasePath(_hostingEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostingEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile("appsettings.local.json", optional: true, reloadOnChange: true);
            configurationBuilder.AddEnvironmentVariables();

            Configuration = configurationBuilder.Build();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<DataContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddIdentity<User, Role>()
                .AddEntityFrameworkStores<DataContext>()
                .AddDefaultTokenProviders();

            var client = ClientBuilder.SPA("Cynosura.Template")
                .WithScopes(IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "api")
                .WithRedirectUri("http://localhost:4200")
                .WithLogoutRedirectUri("http://localhost:4200")
                .Build();
            client.AllowedGrantTypes.Add(GrantType.ResourceOwnerPassword);
            client.AllowedCorsOrigins.Add("http://localhost:4200");
            client.AllowOfflineAccess = true;
    //        client.Claims = new List<Claim>
    //{
    //    new Claim(Constants.ClaimTypes.Name),
    //    new ScopeClaim(Constants.ClaimTypes.Role)
    //};

            services.AddIdentityServer()
                .AddApiAuthorization<User, DataContext>()
                .AddInMemoryApiResources(new []
                {
                    new ApiResource("api", "API", new List<string> 
                    { 
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role
                    })
                })
                //.AddInMemoryIdentityResources(new IdentityResource[] 
                //{ 
                //    new IdentityResources.OpenId(),
                //    new IdentityResources.Profile(),
                //})
                .AddInMemoryClients(new[] { client });

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddMvc()
                //.SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddMvcOptions(o =>
                {
                    o.Filters.Add(typeof(ExceptionLoggerFilter), 10);
                    o.ModelBinderProviders.Insert(0, new UserInfoModelBinderProvider());
                });

            services.AddCors();

            var builder = new ContainerBuilder();
            AutofacConfig.ConfigureAutofac(builder, Configuration);
            builder.Populate(services);
            var applicationContainer = builder.Build();
            return new AutofacServiceProvider(applicationContainer);
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseCors(builder =>
            {
                builder.WithOrigins(Configuration["Cors:Origin"])
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            });

            app.UseAuthentication();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
