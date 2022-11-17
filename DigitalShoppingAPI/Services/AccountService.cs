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
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly DigitalShoppingDbContext context;
        private readonly IMapper mapper;

        public AccountService(UserManager<IdentityUser> userManager,
                SignInManager<IdentityUser> signInManager,
                IConfiguration configuration,
                DigitalShoppingDbContext context,
                IMapper mapper)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.context = context;
            this.mapper = mapper;
        }
    }
}
