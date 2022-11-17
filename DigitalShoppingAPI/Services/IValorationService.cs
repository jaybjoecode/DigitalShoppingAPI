using DigitalShoppingAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.Services
{
    public interface IValorationService
    {
        Task Post(ValorationCreateDTO dto, string userId);
    }
}
