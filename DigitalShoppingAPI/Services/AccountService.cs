using AutoMapper;
using Microsoft.Extensions.Configuration;
using DigitalShoppingAPI.DTOs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using DigitalShoppingAPI.Helpers;
using Microsoft.EntityFrameworkCore;
using DigitalShoppingAPI.Entities;

namespace DigitalShoppingAPI.Services
{
    public class AccountService : IAccountService
    {
        private readonly DigitalShoppingDbContext context;
        private readonly IMapper mapper;

        public AccountService(DigitalShoppingDbContext context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        public async Task<List<UserDTO>> GetListUsers(PaginationDTO paginationDTO)
        {
            var queryable = context.Users.AsQueryable();
            var users = await queryable.OrderBy(x => x.Email).Paginate(paginationDTO).ToListAsync();
            var result = mapper.Map<List<UserDTO>>(users);
            return result;
        }
    }
}
