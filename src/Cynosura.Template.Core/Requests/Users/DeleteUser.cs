using MediatR;

namespace Cynosura.Template.Core.Requests.Users
{
    public class DeleteUser : IRequest
    {
        public int Id { get; set; }
    }
}
