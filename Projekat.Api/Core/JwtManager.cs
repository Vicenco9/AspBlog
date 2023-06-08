using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Projekat.EfDataAccess;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Projekat.Api.Core
{
    public class JwtManager
    {
        private readonly ProjekatContext _context;
        private readonly JwtSettings _settings;
        public JwtManager(ProjekatContext context, JwtSettings settings)
        {
            _context = context;
            _settings = settings;
        }
        public string MakeToken(string username, string password)
        {
            var user = _context.Users.Include(u => u.UserUseCases).FirstOrDefault(x => x.Username == username);

            if (user == null)
            {
                return null;
            }

            //var valid = BCrypt.Net.BCrypt.Verify(password, user.Password);

            //if (!valid)
            //{
            //    throw new UnauthorizedAccessException();
            //}

            var actor = new JwtUser
            {
                Id = user.Id,
                AllowedUseCases = user.UserUseCases.Select(x => x.UseCaseId),
                Indenty = user.Username
            };

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString(),ClaimValueTypes.String, _settings.Issuer),
                new Claim(JwtRegisteredClaimNames.Iss, "asp_api", ClaimValueTypes.String, _settings.Issuer),
                new Claim(JwtRegisteredClaimNames.Iat,DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(),ClaimValueTypes.Integer),
                new Claim("UserId",actor.Id.ToString(),ClaimValueTypes.String, _settings.Issuer),
                new Claim("UserData", JsonConvert.SerializeObject(actor),ClaimValueTypes.String,_settings.Issuer)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_settings.SecretKey));
            var crendentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var now = DateTime.UtcNow;

            var token = new JwtSecurityToken
            (
                issuer: _settings.Issuer,
                audience: "Any",
                claims: claims,
                notBefore: now,
                expires: now.AddMinutes(_settings.Minutes),
                signingCredentials: crendentials
            );
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
