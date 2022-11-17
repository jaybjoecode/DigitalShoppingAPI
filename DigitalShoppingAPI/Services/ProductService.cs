using AutoMapper;
using DigitalShoppingAPI.DTOs.Criterial;
using DigitalShoppingAPI.Entities;
using DigitalShoppingAPI.Helpers;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.Services
{
    public class ProductService : IProductService
    {
        private readonly DigitalShoppingDbContext context;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorageService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly string containerName = "products";
        public ProductService(DigitalShoppingDbContext context,
            UserManager<IdentityUser> userManager,
            IFileStorageService fileStorageSevice,
            IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.fileStorageService = fileStorageSevice;
            this.mapper = mapper;
        }

        public Task<PagedResult<Product>> GetAll(ProductCriterial criterial)
        {
            var queryable =
                context.Products
                .OrderBy(a => a.Title)
                .AsQueryable();

            if (!string.IsNullOrEmpty(criterial.Q))
            {
                string q = criterial.Q
                    .Trim().ToLower();

                queryable = queryable
                    .Where(p => p.Title.ToLower().Contains(q)
                    || p.Description.ToLower().Contains(q));
            }

            var result = new PagedResult<Product>(queryable, criterial);

            return Task.FromResult(result);
        }
    }
}
