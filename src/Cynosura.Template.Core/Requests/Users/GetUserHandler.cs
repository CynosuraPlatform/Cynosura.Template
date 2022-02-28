﻿using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Requests.Users.Models;

namespace Cynosura.Template.Core.Requests.Users
{
    public class GetUserHandler : IRequestHandler<GetUser, UserModel?>
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public GetUserHandler(UserManager<User> userManager, RoleManager<Role> roleManager, IMapper mapper)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _mapper = mapper;
        }

        public async Task<UserModel?> Handle(GetUser request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.Id.ToString());
            if (user == null)
            {
                return null;
            }
            var model = _mapper.Map<User, UserModel>(user);
            var userRoleNames = await _userManager.GetRolesAsync(user);

            foreach (var roleName in userRoleNames)
            {
                var role = await _roleManager.FindByNameAsync(roleName);
                model.RoleIds.Add(role.Id);
            }

            return model;
        }
    }
}
