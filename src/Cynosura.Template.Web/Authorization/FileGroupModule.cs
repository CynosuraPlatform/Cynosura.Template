using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Cynosura.Web.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace Cynosura.Template.Web.Authorization
{
    public class FileGroupModule : IPolicyModule
    {
        public void RegisterPolicies(AuthorizationOptions options)
        {
            options.AddPolicy("ReadFileGroup",
                policy => policy.RequireClaim(ClaimTypes.Role, "Administrator"));
            options.AddPolicy("WriteFileGroup",
                policy => policy.RequireClaim(ClaimTypes.Role, "Administrator"));
        }
    }
}