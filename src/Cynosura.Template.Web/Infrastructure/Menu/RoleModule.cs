using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cynosura.Web.Infrastructure.Menu;

namespace Cynosura.Template.Web.Infrastructure.Menu
{
    public class RoleModule : IMenuModule
    {
        public IEnumerable<MenuItem> GetMenuItems()
        {
            return new List<MenuItem>()
            {
                new MenuItem("./role", "Roles", "glyphicon-lock", new List<string>() { "Administrator" })
            };
        }
    }
}
