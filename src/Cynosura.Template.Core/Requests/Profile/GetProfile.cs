using MediatR;
using Cynosura.Template.Core.Requests.Profile.Models;

namespace Cynosura.Template.Core.Requests.Profile
{
    public class GetProfile : IRequest<ProfileModel>
    {
    }
}
