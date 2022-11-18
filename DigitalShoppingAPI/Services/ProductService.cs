using AutoMapper;
using DigitalShoppingAPI.DTOs;
using DigitalShoppingAPI.DTOs.Criterial;
using DigitalShoppingAPI.Entities;
using DigitalShoppingAPI.Helpers;
using DigitalShoppingAPI.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
        private readonly IUnitOfWork _uow;
        public ProductService(DigitalShoppingDbContext context,
            UserManager<IdentityUser> userManager,
            IFileStorageService fileStorageSevice,
            IMapper mapper,
            IUnitOfWork uow)
        {
            this.context = context;
            this.userManager = userManager;
            this.fileStorageService = fileStorageSevice;
            this.mapper = mapper;
            this._uow = uow;
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

        public async Task Create(ProductCreateDTO productDTO, string userId)
        {
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
        }

        public async Task<ProductDTO> Get(int Id)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == Id);


            if(product == null)
            {
                throw new NotImplementedException();
            }

            var result = mapper.Map<ProductDTO>(product);

            var productPhotos = context.ProductPhotos.Where(b => b.ProductId == Id).ToList();
            if (productPhotos == null)
            {
                result.ProductPhotos = null;
            }
            else
            {
                var photosDTO = mapper.Map<List<ProductPhotosDTO>>(productPhotos);
                result.ProductPhotos = photosDTO;
            }

            var valorations = context.Valorations.Where(v => v.ProductId == Id).ToList();
            if (valorations == null)
            {
                result.valorations = null;
            }
            else
            {
                var valorationsDTO = mapper.Map<List<ValorationDTO>>(valorations);
                foreach (var item in valorationsDTO)
                {
                    var user = await userManager.FindByIdAsync(item.UserId);
                    var profile = await context.Profiles.FirstOrDefaultAsync(x => x.UserId == item.UserId);
                    item.Email = user.Email;
                    item.Avatar = profile.Avatar;
                }
                result.valorations = valorationsDTO;
            }

            return result;
        }

        public async Task Edit(int Id, [FromForm] ProductCreateDTO dto)
        {
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
        }

        public async Task Delete(int Id)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == Id);

            if (product == null)
            {
                throw new NotImplementedException();
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

        }

        public async Task AddPhoto(AddPhotoDTO addPhotoDTO)
        {
            var product = await context.Products.FirstOrDefaultAsync(x => x.Id == addPhotoDTO.ProductId);
            
            if (product == null)
            {
                throw new NotImplementedException();
            }

            var productPhoto = new ProductPhoto();
            productPhoto.Photo = await fileStorageService.SaveFile(containerName, addPhotoDTO.Photo);
            productPhoto.Product = product;
            context.Add(productPhoto);
            await context.SaveChangesAsync();
        }

        public async Task DeletePhoto(int Id)
        {
            var productPhoto = await context.ProductPhotos.FirstOrDefaultAsync(x => x.Id == Id);
            
            if (productPhoto == null)
            {
                throw new NotImplementedException();
            }
            
            context.Remove(productPhoto);
            await context.SaveChangesAsync();
            await fileStorageService.DeleteFile(productPhoto.Photo, containerName);
        }

        public async Task<Product> TestGetOneGR(int Id)
        {
            var user = await _uow.ProductRepository.GetAsync(Id);
            if (user == null)
            {
                throw new NotImplementedException();
            }

            return user;
        }
    }
}
