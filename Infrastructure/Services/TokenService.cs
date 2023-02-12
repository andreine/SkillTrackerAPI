using Domain.Entities.Identity;
using Domain.Interfaces;
using Infrastructure.Identity;
using Infrastructure.Persistance;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration _config;
        private readonly SymmetricSecurityKey _key;
        private readonly UserManager<AppUser> _userManager;

        public TokenService(IConfiguration config, UserManager<AppUser> userManager)
        {
            _config = config;
            _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Token:Key"]));
            _userManager = userManager;

        }

        public async Task<string> CreateToken(AppUser user)
        {
            var userRole = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Email),
            };


            if(userRole == "Manager")
            {
                claims.Add(new Claim("permissions", "manage-skills"));
            }
            else if(userRole == "Employee")
            {
                claims.Add(new Claim("permissions", "solve-tests"));
            }
            else if(userRole == "Reviewer")
            {
                claims.Add(new Claim("permissions", "review-skills"));

            }

            var creds = new SigningCredentials(_key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = creds,
                Issuer = _config["Token:Issuer"]
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}
