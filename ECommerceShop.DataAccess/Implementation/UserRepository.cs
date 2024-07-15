using ECommerceShop.DataAccess.Data;
using ECommerceShop.Entities.Models.Domain;
using ECommerceShop.Entities.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceShop.DataAccess.Implementation
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly IConfiguration _configuration;
        public UserRepository(ApplicationDbContext context, UserManager<User> userManager, IConfiguration configuration)
        : base(context)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public async Task<bool> CheckPasswordAsync(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public async Task<IdentityResult> CreateAsync(User user, string password)
        {
            return await _userManager.CreateAsync(user, password);
        }
        //Identity
        public async Task<User> FindByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }


        public string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:SecretKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id)
        };

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:Issuer"],
                audience: _configuration["JWT:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

    }
}
