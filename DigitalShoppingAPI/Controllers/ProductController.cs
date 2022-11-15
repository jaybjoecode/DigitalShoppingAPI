using AutoMapper;
using DigitalShoppingAPI.DTOs;
using DigitalShoppingAPI.Entities;
using DigitalShoppingAPI.Helpers;
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
        private readonly DigitalShoppingDbContext context;
        private readonly IMapper mapper;
        private readonly IFileStorageService fileStorageService;
        private readonly UserManager<IdentityUser> userManager;
        private readonly string containerName = "products";
        public ProductController(DigitalShoppingDbContext context,
            UserManager<IdentityUser> userManager,
            IFileStorageService fileStorageSevice,
            IMapper mapper)
        {
            this.context = context;
            this.userManager = userManager;
            this.fileStorageService = fileStorageSevice;
            this.mapper = mapper;
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<ActionResult> Create([FromForm] ProductCreateDTO productDTO)
        {
            var email = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var user = await userManager.FindByEmailAsync(email);
            var userId = user.Id;

            var product = mapper.Map<Product>(productDTO);
            product.UserId = userId;
            product.CreatedAt = DateTime.Now;

            if (productDTO.Cover != null)
            {
                product.Cover = await fileStorageService.SaveFile(containerName, productDTO.Cover);
            }
            context.Add(product);

            if (productDTO.ProductPhotos != null)
            {
                foreach (var photo in productDTO.ProductPhotos)
                {
                    var productPhoto = new ProductPhoto();
                    productPhoto.Photo = await fileStorageService.SaveFile(containerName, photo);
                    productPhoto.Product = product;
                    context.Add(productPhoto);
                }
            }

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpGet]
        public async Task<ActionResult<List<ProductDTO>>> GetAll([FromQuery] PaginationDTO paginationDTO)
        {
            var queryable = context.Products.AsQueryable();
            await HttpContext.InsertParametersPaginationInHeader(queryable);
            var products = await queryable.OrderBy(x => x.Title).Paginate(paginationDTO).ToListAsync();
            var result = mapper.Map<List<ProductDTO>>(products);

            return Ok(result);
        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<ProductDTO>> Get(int Id)
        {
            var client = await context.Products.FirstOrDefaultAsync(x => x.Id == Id);
            if (client == null)
            {
                return NotFound();
            }
            var result = mapper.Map<ProductDTO>(client);

            return Ok(result);
        }

        [HttpPut("{Id:int}")]
        public async Task<ActionResult> Edit(int Id, [FromForm] ProductCreateDTO dto)
        {
            //var product = mapper.Map<Product>(dto);
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == Id);
            product.Title = dto.Title;
            product.Description = dto.Description;
            product.Id = Id;
            if (dto.Cover != null)
            {
                product.Cover = await fileStorageService
                    .EditFile(containerName, dto.Cover, product.Cover);
            }
            context.Entry(product).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{Id:int}")]
        public async Task<ActionResult> Delete(int Id)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == Id);
            if (product == null)
            {
                return NotFound();
            }

            var productPhotos = context.ProductPhotos.Where(b => b.ProductId == Id).ToList();

            if (productPhotos != null)
            {
                foreach (var photo in productPhotos)
                {
                    await fileStorageService.DeleteFile(photo.Photo, containerName);
                }
            }
            context.Remove(product);
            await context.SaveChangesAsync();
            await fileStorageService.DeleteFile(product.Cover, containerName);

            return Ok();
        }

        [HttpPost("/photo")]
        public async Task<ActionResult> AddPhoto([FromForm] AddPhotoDTO addPhotoDTO)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == addPhotoDTO.ProductId);
            if (product == null)
            {
                return NotFound();
            }

            var productPhoto = new ProductPhoto();
            productPhoto.Photo = await fileStorageService.SaveFile(containerName, addPhotoDTO.Photo);
            productPhoto.Product = product;
            context.Add(productPhoto);

            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("/photo/{Id:int}")]
        public async Task<ActionResult> DeletePhoto(int Id)
        {
            var productPhoto = await context.ProductPhotos.FirstOrDefaultAsync(x => x.Id == Id);
            if (productPhoto == null)
            {
                return NotFound();
            }

            context.Remove(productPhoto);
            await context.SaveChangesAsync();
            await fileStorageService.DeleteFile(productPhoto.Photo, containerName);

            return Ok();
        }
    }
}
