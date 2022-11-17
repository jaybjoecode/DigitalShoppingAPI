using AutoMapper;
using DigitalShoppingAPI.DTOs;
using DigitalShoppingAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.Services
{
    public class ShoppingCarService : IShoppingCarService
    {
        private readonly DigitalShoppingDbContext context;
        private readonly IMapper mapper;
        public ShoppingCarService(DigitalShoppingDbContext context,
            IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<ShoppingCarDTO>> Get(string userId)
        {
            var shoppingcar = context.ShoppingCars.Where(x => x.UserId == userId).ToList();

            var result = mapper.Map<List<ShoppingCarDTO>>(shoppingcar);

            foreach (var item in result)
            {
                var product = await context.Products.FirstOrDefaultAsync(x => x.Id == item.Product.Id);
                item.Product = mapper.Map<ProductDTO>(product);
            }

            return result;
        }

        public async Task Post(int ProductId, string userId)
        {
            var shoppingCar = new ShoppingCar();
            shoppingCar.UserId = userId;
            shoppingCar.ProductId = ProductId;

            context.Add(shoppingCar);

            await context.SaveChangesAsync();
        }

        public async Task Delete(int Id)
        {
            var shoppingCar = await context.ShoppingCars.FirstOrDefaultAsync(x => x.Id == Id);
            if (shoppingCar == null)
            {
                throw new NotImplementedException();
            }

            context.Remove(shoppingCar);
            await context.SaveChangesAsync();
        }
    }
}
