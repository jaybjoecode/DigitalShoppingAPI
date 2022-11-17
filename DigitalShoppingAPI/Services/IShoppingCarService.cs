using DigitalShoppingAPI.DTOs;
using DigitalShoppingAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.Services
{
    public interface IShoppingCarService
    {
        Task<List<ShoppingCarDTO>> Get(string userId);
        Task Post(int ProductId, string userId);
        Task Delete(int Id);
    }
}
