﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cynosura.Core.Services.Models;
using Cynosura.Template.Core.Infrastructure;
using Cynosura.Template.Core.Requests.Roles;
using Cynosura.Template.Core.Requests.Roles.Models;
using Cynosura.Template.Web.Protos;
using Cynosura.Template.Web.Protos.Roles;
using Grpc.Core;
using MediatR;
using Microsoft.AspNetCore.Authorization;

namespace Cynosura.Template.Web.Services
{
    [Authorize("ReadRole")]
    public class RoleService : Protos.Roles.RoleService.RoleServiceBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public RoleService(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        public override async Task<RolePageModel> GetRoles(GetRolesRequest getRolesRequest, ServerCallContext context)
        {
            var getRoles = _mapper.Map<GetRolesRequest, GetRoles>(getRolesRequest);
            return _mapper.Map<PageModel<RoleModel>, RolePageModel>(await _mediator.Send(getRoles));
        }

        public override async Task<Role> GetRole(GetRoleRequest getRoleRequest, ServerCallContext context)
        {
            var getRole = _mapper.Map<GetRoleRequest, GetRole>(getRoleRequest);
            return _mapper.Map<RoleModel, Role>(await _mediator.Send(getRole));
        }

        [Authorize("WriteRole")]
        public override async Task<Empty> UpdateRole(UpdateRoleRequest updateRoleRequest, ServerCallContext context)
        {
            var updateRole = _mapper.Map<UpdateRoleRequest, UpdateRole>(updateRoleRequest);
            return _mapper.Map<Unit, Empty>(await _mediator.Send(updateRole));
        }

        [Authorize("WriteRole")]
        public override async Task<CreatedEntity> CreateRole(CreateRoleRequest createRoleRequest, ServerCallContext context)
        {
            var createRole = _mapper.Map<CreateRoleRequest, CreateRole>(createRoleRequest);
            return _mapper.Map<CreatedEntity<int>, CreatedEntity>(await _mediator.Send(createRole));
        }

        [Authorize("WriteRole")]
        public override async Task<Empty> DeleteRole(DeleteRoleRequest deleteRoleRequest, ServerCallContext context)
        {
            var deleteRole = _mapper.Map<DeleteRoleRequest, DeleteRole>(deleteRoleRequest);
            return _mapper.Map<Unit, Empty>(await _mediator.Send(deleteRole));
        }
    }
}
