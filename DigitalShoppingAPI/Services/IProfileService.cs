using DigitalShoppingAPI.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.Services
{
    public interface IProfileService
    {
        Task<ProfileInfoDTO> Get(string UserId);
        Task Edit(int Id, ProfileInfoUpdateDTO dto);
        Task Avatar(int Id, AvatarDTO dto);
    }
}
