using AutoMapper;
using DigitalShoppingAPI.DTOs;
using DigitalShoppingAPI.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.Services
{
    public class ProfilesService : IProfileService
    {
        private readonly DigitalShoppingDbContext context;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorageService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly string containerName = "profiles";

        public ProfilesService(DigitalShoppingDbContext context, 
            IMapper mapper, 
            IFileStorageService fileStorageService,
            UserManager<IdentityUser> userManager)
        {
            this.context = context;
            this.mapper = mapper;
            this.fileStorageService = fileStorageService;
            this.userManager = userManager;
        }
        public async Task<ProfileInfoDTO> Get(string UserId)
        {
            var profile = await context.Profiles.FirstOrDefaultAsync(x => x.UserId == UserId);
            if (profile == null)
            {
                throw new NotImplementedException();
            }
            var result = mapper.Map<ProfileInfoDTO>(profile);
            var user = await userManager.FindByIdAsync(result.UserId);
            result.Email = user.Email;

            return result;
        }

        public async Task Edit(int Id, ProfileInfoUpdateDTO dto)
        {
            var profileInfo = await context.Profiles.FirstOrDefaultAsync(x => x.Id == Id);
            profileInfo.Name = dto.Name;
            profileInfo.LastName = dto.LastName;
            profileInfo.Id = Id;
        }

        public async Task Avatar(int Id, AvatarDTO dto)
        {
            var profileInfo = await context.Profiles.FirstOrDefaultAsync(x => x.Id == Id);

            if (dto.Avatar != null)
            {
                profileInfo.Avatar = await fileStorageService
                    .EditFile(containerName, dto.Avatar, profileInfo.Avatar);
            }
            context.Entry(profileInfo).State = EntityState.Modified;
            await context.SaveChangesAsync();
        }
    }
}
