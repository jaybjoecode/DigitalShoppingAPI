using AutoMapper;
using DigitalShoppingAPI.DTOs;
using DigitalShoppingAPI.Helpers;
using DigitalShoppingAPI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService service;
        public ProfileController(IProfileService service)
        {
            this.service = service;
        }

        [HttpGet("{UserId}")]
        public async Task<ActionResult<ProfileInfoDTO>> Get(string UserId)
        {
            var result = await service.Get(UserId);

            return Ok(result);
        }

        [HttpPut("{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Edit(int Id, [FromForm] ProfileInfoUpdateDTO dto)
        {
            await service.Edit(Id, dto);

            return Ok();
        }

        [HttpPut("avatar/{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Avatar(int Id, [FromForm] AvatarDTO dto)
        {
            await service.Avatar(Id, dto);

            return Ok();
        }
    }
}
