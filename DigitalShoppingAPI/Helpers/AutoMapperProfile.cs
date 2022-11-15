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
        }
    }
}
