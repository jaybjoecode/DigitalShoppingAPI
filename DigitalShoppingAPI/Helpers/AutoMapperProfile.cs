using AutoMapper;
using DigitalShoppingAPI.DTOs;
using DigitalShoppingAPI.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DigitalShoppingAPI.Helpers
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<IdentityUser, UserDTO>();
            CreateMap<ProductCreateDTO, Product>()
                .ForMember(x => x.Cover, options => options.Ignore())
                .ForMember(x => x.ProductPhotos, options => options.Ignore());
            CreateMap<ProductDTO, Product>().ReverseMap();
            CreateMap<ProductPhotosDTO, ProductPhoto>().ReverseMap();
            CreateMap<ShoppingCarDTO, ShoppingCar>().ReverseMap();
            CreateMap<ValorationDTO, Valoration>().ReverseMap();
            CreateMap<ProfileInfoUpdateDTO, ProfileInfo>().ReverseMap();
            CreateMap<ProfileInfoDTO, ProfileInfo>().ReverseMap();
        }

        private ProductDTO MapShoppingCarProduct(Product arg)
        {
            var result = new ProductDTO()
            {
                Id = arg.Id,
                UserId = arg.UserId,
                Cover = arg.Cover,
                Title = arg.Title,
                Description = arg.Description,
                Rating = arg.Rating,
                CreatedAt = arg.CreatedAt
            };

            return result;
        }

        private ProductDTO MapShoppingCarProduct2(ShoppingCar shopping, ShoppingCarDTO shoppingdto)
        {
            if (shopping.Product != null)
            {
                var result = new ProductDTO()
                {
                    Id = shopping.Product.Id,
                    UserId = shopping.Product.UserId,
                    Cover = shopping.Product.Cover,
                    Title = shopping.Product.Title,
                    Description = shopping.Product.Description,
                    Rating = shopping.Product.Rating,
                    CreatedAt = shopping.Product.CreatedAt
                };

                return result;
            }

            return null;
        }
    }
}
