using System;
using System.Collections.Generic;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Data;
using Cynosura.Template.Web.Infrastructure;
using Cynosura.Web;
using Cynosura.Web.Infrastructure.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;

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

            services.AddDefaultIdentity<User>()
                .AddRoles<Role>()
                .AddEntityFrameworkStores<DataContext>();

            services.AddIdentityServer()
                .AddApiAuthorization<User, DataContext>()
                .AddProfileService<MyProfileService>();

            services.AddAuthentication()
                .AddIdentityServerJwt();

            services.AddMvc()
                .AddMvcOptions(o =>
                {
                    o.Filters.Add(typeof(ExceptionLoggerFilter), 10);
                    o.ModelBinderProviders.Insert(0, new UserInfoModelBinderProvider());
                })
                .AddJsonOptions(o =>
                {
                    o.JsonSerializerOptions.IgnoreNullValues = true;
                    o.JsonSerializerOptions.Converters.Add(new TimeSpanConverter());
                });

            services.AddAuthorization(options =>
            {
                new PolicyProvider().RegisterPolicies(options);
            });

            services.AddCors();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Cynosura.Template API", Version = "v1" });
                c.AddFluentValidationRules();
                c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.OAuth2,
                    Flows = new OpenApiOAuthFlows
                    {
                        AuthorizationCode = new OpenApiOAuthFlow
                        {
                            AuthorizationUrl = new Uri("/connect/authorize", UriKind.Relative),
                            TokenUrl = new Uri("/connect/token", UriKind.Relative),
                            Scopes = new Dictionary<string, string>
                            {
                                { "Cynosura.Template.WebAPI", "" },
                                { "openid", "" },
                                { "profile", "" },
                            }
                        }
                    }
                });
                c.OperationFilter<SecurityRequirementsOperationFilter>();
            });

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

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Cynosura.Template API V1");
                c.OAuthClientId("Swagger");
                c.OAuthAppName("Cynosura.Template.Web");
                c.OAuthScopeSeparator(" ");
                c.OAuthUsePkce();
                c.ConfigObject.DeepLinking = true;
            });

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
                endpoints.MapRazorPages();
            });
        }
    }
}
