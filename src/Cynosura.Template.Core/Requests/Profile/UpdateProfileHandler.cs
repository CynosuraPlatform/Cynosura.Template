﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Cynosura.Core.Data;
using Cynosura.Template.Core.Entities;
using Cynosura.Template.Core.Requests.Users;
using Cynosura.Template.Core.Security;
using Cynosura.Core.Services;
using Microsoft.Extensions.Localization;

namespace Cynosura.Template.Core.Requests.Profile
{
    public class UpdateProfileHandler : IRequestHandler<UpdateProfile>
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserInfoProvider _userInfoProvider;
        private readonly IMapper _mapper;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public UpdateProfileHandler(
            UserManager<User> userManager,
            IUserInfoProvider userInfoProvider,
            IMapper mapper,
            IStringLocalizer<SharedResource> localizer)
        {
            _userManager = userManager;
            _userInfoProvider = userInfoProvider;
            _mapper = mapper;
            _localizer = localizer;
        }

        public async Task Handle(UpdateProfile request, CancellationToken cancellationToken)
        {
            var userInfo = await _userInfoProvider.GetUserInfoAsync();
            var user = await _userManager.Users
                .FirstOrDefaultAsync(e => e.Id == userInfo.UserId, cancellationToken);
            if (user == null)
            {
                throw new ServiceException(_localizer["{0} not found", _localizer["User"]]);
            }
            _mapper.Map(request, user);

            var result = await _userManager.UpdateAsync(user);
            result.CheckIfSucceeded();
        }
    }
}
