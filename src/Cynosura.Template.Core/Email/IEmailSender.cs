using System.Threading.Tasks;
using Cynosura.Template.Core.Email.Models;

namespace Cynosura.Template.Core.Email
{
    public interface IEmailSender
    {
        Task SendAsync(EmailModel model);
    }
}
