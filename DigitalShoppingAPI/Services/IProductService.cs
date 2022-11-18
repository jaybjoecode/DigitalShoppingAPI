using DigitalShoppingAPI.DTOs;
using DigitalShoppingAPI.DTOs.Criterial;
using DigitalShoppingAPI.Entities;
using DigitalShoppingAPI.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.Services
{
    public interface IProductService
    {
        Task<PagedResult<Product>> GetAll(ProductCriterial criterial);
        Task Create(ProductCreateDTO productDTO, string userId);
        Task<ProductDTO> Get(int Id);
        Task Edit(int Id, ProductCreateDTO dto);
        Task Delete(int Id);
        Task AddPhoto(AddPhotoDTO addPhotoDTO);
        Task DeletePhoto(int Id);
        Task<Product> TestGetOneGR(int Id);
    }
}
