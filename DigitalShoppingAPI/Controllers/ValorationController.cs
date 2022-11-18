using AutoMapper;
using DigitalShoppingAPI.DTOs;
using DigitalShoppingAPI.Entities;
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
using System.Security.Claims;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ValorationController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly ValorationService service;
        public ValorationController(UserManager<IdentityUser> userManager, ValorationService service)
        {
            this.userManager = userManager;
            this.service = service;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] ValorationCreateDTO dto)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = await userManager.FindByEmailAsync(email);
            var userId = user.Id;

            await service.Post(dto, userId);

            return Ok();
        }
    }
}
