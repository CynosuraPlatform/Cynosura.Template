using System;
using System.Collections.Generic;
using System.Text;
using Cynosura.Template.Core.Email;
using Cynosura.Template.Infrastructure.Email;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cynosura.Template.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<EmailSenderOptions>(configuration.GetSection("EmailSender"));
            return services;
        }
    }
}
