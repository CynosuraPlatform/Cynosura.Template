using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;
using Cynosura.Web.Infrastructure;

namespace Cynosura.Template.Web.Routes
{
    public class WorkerRunModule : IConfigurationModule<IEndpointRouteBuilder>
    {
        public void Configure(IEndpointRouteBuilder configuration)
        {
            configuration.MapGrpcService<Services.WorkerRunService>();
        }
    }
}