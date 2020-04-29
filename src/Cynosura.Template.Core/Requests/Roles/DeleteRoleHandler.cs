using System.Threading;
using System.Threading.Tasks;
using Cynosura.Core.Services;
using Cynosura.Template.Core.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Cynosura.Template.Core.Requests.Roles
{
    public class DeleteRoleHandler : IRequestHandler<DeleteRole>
    {
        private readonly RoleManager<Role> _roleManager;

        public DeleteRoleHandler(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<Unit> Handle(DeleteRole request, CancellationToken cancellationToken)
        {
            var role = await _roleManager.FindByIdAsync(request.Id.ToString());
            if (role == null)
            {
                throw new ServiceException($"Role {request.Id} not found");
            }
            var result = await _roleManager.DeleteAsync(role);
            result.CheckIfSucceeded();
            return Unit.Value;
        }
    }
}
