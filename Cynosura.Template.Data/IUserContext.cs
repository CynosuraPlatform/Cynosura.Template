using System.Threading.Tasks;
using Cynosura.Template.Core.Entities;

namespace Cynosura.Template.Data
{
    public interface IUserContext
    {
        Task<User> GetUserAsync();
    }
}
