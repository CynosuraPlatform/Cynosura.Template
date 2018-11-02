using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Cynosura.Template.Core.Requests.Roles
{
    public class CreateRole : IRequest<int>
    {
        [Required]
        public string Name { get; set; }
    }
}
