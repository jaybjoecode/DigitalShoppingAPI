using DigitalShoppingAPI.DTOs;
using DigitalShoppingAPI.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.Services
{
    public class ValorationService : IValorationService
    {
        private readonly DigitalShoppingDbContext context;
        public ValorationService(DigitalShoppingDbContext context)
        {
            this.context = context;
        }

        public async Task Post(ValorationCreateDTO dto, string userId)
        {
            var shoppingCar = new Valoration()
            {
                UserId = userId,
                ProductId = dto.ProductId,
                Comment = dto.Comment,
                Rating = dto.Rating,
                CreatedAt = DateTime.Now,
            };

            context.Add(shoppingCar);

            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == dto.ProductId);
            var profile = await context.Profiles.FirstOrDefaultAsync(x => x.UserId == product.UserId);

            product.Rating = (product.Rating > 0)
                ? (int)(product.Rating + dto.Rating) / 2
                : dto.Rating;
            context.Entry(product).State = EntityState.Modified;

            profile.Rating = (profile.Rating > 0)
                ? (int)(profile.Rating + product.Rating) / 2
                : product.Rating;

            context.Entry(profile).State = EntityState.Modified;

            await context.SaveChangesAsync();
        }
    }
}
