using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Cynosura.Template.Core.Email;
using Cynosura.Template.Core.Formatters;
using Cynosura.Template.Infrastructure.Email;
using Cynosura.Template.Infrastructure.Formatters;

namespace Cynosura.Template.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddTransient<IEmailSender, EmailSender>();
            services.Configure<EmailSenderOptions>(configuration.GetSection("EmailSender"));
            services.AddTransient<IExcelFormatter, ExcelFormatter>();
            return services;
        }
    }
}
