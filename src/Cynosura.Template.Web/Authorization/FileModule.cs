using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cynosura.Web.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Cynosura.Template.Web.Authorization
{
    public class FileModule : IPolicyModule
    {
        public void RegisterPolicies(AuthorizationOptions options)
        {
            options.AddPolicy("ReadFile",
                policy => policy.RequireClaim(ClaimTypes.Role, "Administrator"));
            options.AddPolicy("WriteFile",
                policy => policy.RequireClaim(ClaimTypes.Role, "Administrator"));
        }
    }
}