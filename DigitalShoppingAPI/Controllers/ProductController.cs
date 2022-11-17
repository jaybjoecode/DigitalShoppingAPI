using AutoMapper;
using DigitalShoppingAPI.DTOs;
using DigitalShoppingAPI.DTOs.Criterial;
using DigitalShoppingAPI.Entities;
using DigitalShoppingAPI.Helpers;
using DigitalShoppingAPI.Services;
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
    [ApiController]
    [Route("[controller]")]
    public class ProductController : ControllerBase
    {
        private readonly IProductService service;
        private readonly UserManager<IdentityUser> userManager;
        public ProductController(IProductService service, UserManager<IdentityUser> userManager)
        {
            this.service = service;
            this.userManager = userManager;
        }

        [HttpGet]
        public async Task<ActionResult<PagedResult<Product>>> GetAll([FromQuery] ProductCriterial criterial)
        {
            var result = await service.GetAll(criterial);

            return Ok(result);
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Create([FromForm] ProductCreateDTO productDTO)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user = await userManager.FindByEmailAsync(email);
            var userId = user.Id;

            await service.Create(productDTO, userId);

            return Ok();
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProductDTO>> Get(int Id)
        {
            var result = await service.Get(Id);

            return Ok(result);                        
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult> Edit(int Id, ProductCreateDTO dto)
        {
            await service.Edit(Id, dto);

            return Ok();
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
            await service.Delete(Id);

            return Ok();
        }

        [HttpPost("/photo")]
        public async Task<ActionResult> AddPhoto([FromForm] AddPhotoDTO addPhotoDTO)
        {
            await service.AddPhoto(addPhotoDTO);

            return Ok();
        }

        [HttpDelete("/photo/{Id:int}")]
        public async Task<ActionResult> DeletePhoto(int Id)
        {
            await service.DeletePhoto(Id);

            return Ok();
        }
    }
}
