using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using Cynosura.Web.Infrastructure;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Services;
using Cynosura.Template.Core.Services.Models;
using Cynosura.Template.Web.Models;
using Cynosura.Template.Web.Models.RoleViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Cynosura.Template.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize(Roles = "Administrator")]
    [ValidateModel]
    [Route("api/[controller]")]
    public class RoleController : Controller
    {
        private readonly IRoleService _roleService;
        private readonly IMapper _mapper;

        public RoleController(IRoleService roleService, IMapper mapper)
        {
            _roleService = roleService;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<PageModel<RoleViewModel> > GetRolesAsync(int? pageIndex, int? pageSize)
        {
            var roles = await _roleService.GetRolesAsync(pageIndex, pageSize);
            return roles.Map<Role, RoleViewModel>(_mapper);
        }

        [HttpGet("{id:int}")]
        public async Task<RoleViewModel> GetRoleAsync(int id)
        {
            var role = await _roleService.GetRoleAsync(id);
            return _mapper.Map<Role, RoleViewModel>(role);
        }

        [HttpPut("{id:int}")]
        public async Task<StatusViewModel> PutRoleAsync(int id, [FromBody] RoleUpdateViewModel role)
        {
            var model = _mapper.Map<RoleUpdateViewModel, RoleUpdateModel>(role);
            await _roleService.UpdateRoleAsync(id, model);
            return new StatusViewModel();
        }

        [HttpPost("")]
        public async Task<StatusViewModel> PostRoleAsync([FromBody] RoleCreateViewModel role)
        {
            var model = _mapper.Map<RoleCreateViewModel, RoleCreateModel>(role);
            var id = await _roleService.CreateRoleAsync(model);
            return new CreationStatusViewModel(id);
        }

        [HttpDelete("{id:int}")]
        public async Task<StatusViewModel> DeleteRoleAsync(int id)
        {
            await _roleService.DeleteRoleAsync(id);
            return new StatusViewModel();
        }
    }
}