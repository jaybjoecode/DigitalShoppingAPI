using DigitalShoppingAPI.DTOs.Criterial;
using DigitalShoppingAPI.Entities;
using DigitalShoppingAPI.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.Services
{
    public interface IProductService
    {
        Task<PagedResult<Product>> GetAll(ProductCriterial criterial);
    }
}
