using System.Threading.Tasks;
using Cynosura.Core.Services.Models;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Services.Models;

namespace Cynosura.Template.Core.Services
{
    public interface IRoleService
    {
        Task<Role> GetRoleAsync(int id);
        Task<PageModel<Role>> GetRolesAsync(int? pageIndex = null, int? pageSize = null);
        Task<int> CreateRoleAsync(RoleCreateModel model);
        Task UpdateRoleAsync(int id, RoleUpdateModel model);
        Task DeleteRoleAsync(int id);
    }
}
