using AutoMapper;
using DigitalShoppingAPI.DTOs;
using DigitalShoppingAPI.Helpers;
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
    [Route("[controller]")]
    public class ProfileController : ControllerBase
    {
        private readonly DigitalShoppingDbContext context;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorageService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly string containerName = "profiles";
        public ProfileController(DigitalShoppingDbContext context,
            UserManager<IdentityUser> userManager,
            IFileStorageService fileStorageSevice,
            IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.fileStorageService = fileStorageSevice;
            this.mapper = mapper;
        }

        [HttpGet("{UserId}")]
        public async Task<ActionResult<ProfileInfoDTO>> Get(string UserId)
        {
            var profile = await context.Profiles.FirstOrDefaultAsync(x => x.UserId == UserId);
            if (profile == null)
            {
                return NotFound();
            }
            var result = mapper.Map<ProfileInfoDTO>(profile);
            var user = await userManager.FindByIdAsync(result.UserId);
            result.Email = user.Email;

            return Ok(result);
        }

        [HttpPut("{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Edit(int Id, [FromForm] ProfileInfoUpdateDTO dto)
        {
            var profileInfo = await context.Profiles.FirstOrDefaultAsync(x => x.Id == Id);
            profileInfo.Name = dto.Name;
            profileInfo.LastName = dto.LastName;
            profileInfo.Id = Id;

            return Ok();
        }

        [HttpPut("avatar/{Id:int}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Avatar(int Id, [FromForm] AvatarDTO dto)
        {
            var profileInfo = await context.Profiles.FirstOrDefaultAsync(x => x.Id == Id);
            
            if (dto.Avatar != null)
            {
                profileInfo.Avatar = await fileStorageService
                    .EditFile(containerName, dto.Avatar, profileInfo.Avatar);
            }
            context.Entry(profileInfo).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
