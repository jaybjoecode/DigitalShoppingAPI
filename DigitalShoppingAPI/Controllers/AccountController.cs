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
using DigitalShoppingAPI.Services;

namespace DigitalShoppingAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountService : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly SignInManager<IdentityUser> signInManager;
        private readonly IConfiguration configuration;
        private readonly DigitalShoppingDbContext context;
        private readonly IAccountService service;
        public AccountService(UserManager<IdentityUser> userManager,
                SignInManager<IdentityUser> signInManager,
                IConfiguration configuration,
                DigitalShoppingDbContext context,
                IAccountService service)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.configuration = configuration;
            this.context = context;
            this.service = service;
        }

        [HttpPost("register")]
        public async Task<ActionResult<AuthenticationResponse>> Register(
            [FromBody] RegisterDTO userCredencials)
        {
            var user = new IdentityUser { UserName = userCredencials.Email, Email = userCredencials.Email };
            var result = await userManager.CreateAsync(user, userCredencials.Password);            

            if (result.Succeeded)
            {
                var profile = new ProfileInfo()
                {
                    UserId = user.Id,
                    Avatar = null,
                    Name = userCredencials.Name,
                    LastName = userCredencials.LastName
                };

                context.Add(profile);
                await context.SaveChangesAsync();

                return await BuildTokenFromRegister(userCredencials);
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost("create-by-admin")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult<AuthenticationResponse>> CreateByAdmin(
            [FromBody] RegisterDTO userCredencials)
        {
            var user = new IdentityUser { UserName = userCredencials.Email, Email = userCredencials.Email };
            var result = await userManager.CreateAsync(user, userCredencials.Password);

            if (result.Succeeded)
            {
                var profile = new ProfileInfo()
                {
                    UserId = user.Id,
                    Avatar = null,
                    Name = userCredencials.Name,
                    LastName = userCredencials.LastName
                };

                context.Add(profile);
                await context.SaveChangesAsync();

                return Ok();
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        private ActionResult<AuthenticationResponse> BadRequestResult(IEnumerable<IdentityError> errors)
        {
            throw new NotImplementedException();
        }

        [HttpPost("login")]
        public async Task<ActionResult<AuthenticationResponse>> Login([FromBody] LoginDTO logindto)
        {
            var result = await signInManager.PasswordSignInAsync(logindto.Email,
                logindto.Password, isPersistent: false, lockoutOnFailure: false);

            if (result.Succeeded)
            {
                return await BuildToken(logindto);
            }
            else
            {
                return new BadRequestObjectResult("Incorrect login");
            }
        }

        private async Task<AuthenticationResponse> BuildTokenFromRegister(RegisterDTO registerdto)
        {
            var logindto = new LoginDTO()
            { 
                Email = registerdto.Email,
                Password = registerdto.Password
            };
            return await BuildToken(logindto);
        }
        private async Task<AuthenticationResponse> BuildToken(LoginDTO userCredencials)
        {
            // data to send encrypted
            var claims = new List<Claim>()
            {
                new Claim("email", userCredencials.Email)
            };

            var user = await userManager.FindByNameAsync(userCredencials.Email);
            var claimsDB = await userManager.GetClaimsAsync(user);
            // add role data to send encrypted
            claims.AddRange(claimsDB);

            var keyValue = configuration["keyjwt"];
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(keyValue));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiration = DateTime.UtcNow.AddDays(1);
            var token = new JwtSecurityToken(issuer: null, audience: null, claims: claims,
                expires: expiration, signingCredentials: creds);

            return new AuthenticationResponse()
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Expiration = expiration
            };
        }

        [HttpPost("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return Ok("logout");
        }

        [HttpGet("list")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "IsAdmin")]
        public async Task<ActionResult<List<UserDTO>>> GetListUsers([FromQuery] PaginationDTO paginationDTO)
        {
            var result = service.GetListUsers(paginationDTO);

            return Ok(result);
        }
    }
}
