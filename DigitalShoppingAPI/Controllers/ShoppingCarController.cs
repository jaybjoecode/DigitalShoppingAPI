using AutoMapper;
using DigitalShoppingAPI.DTOs;
using DigitalShoppingAPI.Entities;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
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
    public class ShoppingCarController : ControllerBase
    {
        private readonly DigitalShoppingDbContext context;
        private readonly UserManager<IdentityUser> userManager;
        private readonly IMapper mapper;

        public ShoppingCarController(DigitalShoppingDbContext context,
            UserManager<IdentityUser> userManager,
            IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.mapper = mapper;
        }
        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult<List<ShoppingCar>>> Get()
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = await userManager.FindByEmailAsync(email);
            var userId = user.Id;

            var shoppingcar = context.ShoppingCars.Where(x => x.UserId == userId).ToList();

            //var result = mapper.Map<List<ShoppingCarDTO>>(shoppingcar);

            return Ok(shoppingcar);
        }

        // POST api/<ShoppingCarController>
        [HttpPost("{ProductId}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Post(int ProductId)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var user = await userManager.FindByEmailAsync(email);
            var userId = user.Id;

            var shoppingCar = new ShoppingCar();
            shoppingCar.UserId = userId;
            shoppingCar.ProductId = ProductId;

            context.Add(shoppingCar);

            await context.SaveChangesAsync();

            return Ok();
        }

        // DELETE api/<ShoppingCarController>/5
        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Delete(int Id)
        {
            var shoppingCar = await context.ShoppingCars.FirstOrDefaultAsync(x => x.Id == Id);
            if (shoppingCar == null)
            {
                return NotFound();
            }

            context.Remove(shoppingCar);
            await context.SaveChangesAsync();

            return Ok();
        }
    }
}
