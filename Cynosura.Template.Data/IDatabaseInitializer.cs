using System.Threading.Tasks;

namespace Cynosura.Template.Data
{
    public interface IDatabaseInitializer
    {
        Task SeedAsync();
    }
}
