using System.Threading.Tasks;
using Cynosura.Core.Services.Models;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Services.Models;

namespace Cynosura.Template.Core.Services
{
    public interface IUserService
    {        
        Task<User> GetUserAsync(int id);
        Task<PageModel<User>> GetUsersAsync(int? pageIndex = null, int? pageSize = null);
        Task<int> CreateUserAsync(UserCreateModel model);
        Task UpdateUserAsync(int id, UserUpdateModel model);
        Task DeleteUserAsync(int id);
    }
}
