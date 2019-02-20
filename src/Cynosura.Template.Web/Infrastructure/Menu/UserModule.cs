using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cynosura.Web.Infrastructure.Menu;

namespace Cynosura.Template.Web.Infrastructure.Menu
{
    public class UserModule : IMenuModule
    {
        public IEnumerable<MenuItem> GetMenuItems()
        {
            return new List<MenuItem>()
            {
                new MenuItem("./user", "Users", "glyphicon-user", new List<string>() {"Administrator"})
            };
        }
    }
}
