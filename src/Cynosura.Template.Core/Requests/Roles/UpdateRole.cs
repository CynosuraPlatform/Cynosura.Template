using MediatR;

namespace Cynosura.Template.Core.Requests.Roles
{
    public class UpdateRole : IRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
