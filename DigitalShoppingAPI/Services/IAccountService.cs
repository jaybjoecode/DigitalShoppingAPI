using DigitalShoppingAPI.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.Services
{
    public interface IAccountService
    {
        Task<List<UserDTO>> GetListUsers(PaginationDTO paginationDTO);
    }
}
