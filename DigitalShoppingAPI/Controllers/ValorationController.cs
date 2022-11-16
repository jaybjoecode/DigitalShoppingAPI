using AutoMapper;
using DigitalShoppingAPI.DTOs;
using DigitalShoppingAPI.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValorationController : ControllerBase
    {
        private readonly DigitalShoppingDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;
        public ValorationController(DigitalShoppingDbContext context,
            UserManager<IdentityUser> userManager,
            IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post([FromBody] ValorationCreateDTO dto)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = await userManager.FindByEmailAsync(email);
            var userId = user.Id;

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

            return Ok();
        }
    }
}
