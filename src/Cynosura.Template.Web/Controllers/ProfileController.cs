﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Cynosura.Web.Infrastructure;
using Cynosura.Template.Core.Requests.Profile;
using Cynosura.Template.Core.Requests.Profile.Models;

namespace Cynosura.Template.Web.Controllers
{
    [ServiceFilter(typeof(ApiExceptionFilterAttribute))]
    [Authorize]
    [ValidateModel]
    [Route("api")]
    public class ProfileController : Controller
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("GetProfile")]
        public async Task<ProfileModel?> GetProfileAsync(GetProfile getProfile)
        {
            return await _mediator.Send(getProfile);
        }

        [HttpPost("UpdateProfile")]
        public async Task UpdateProfileAsync([FromBody] UpdateProfile updateProfile)
        {
            await _mediator.Send(updateProfile);
        }
    }
}
