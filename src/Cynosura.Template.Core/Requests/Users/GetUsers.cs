using Cynosura.Core.Services.Models;
using Cynosura.Template.Core.Requests.Users.Models;
using MediatR;

namespace Cynosura.Template.Core.Requests.Users
{
    public class GetUsers : IRequest<PageModel<UserModel>>
    {
        public int? PageIndex { get; set; }
        public int? PageSize { get; set; }
    }
}
