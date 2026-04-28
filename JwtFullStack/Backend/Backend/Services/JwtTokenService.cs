using Backend.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;

namespace Backend.Services
{

    public interface IJwtTokenService
    {
        Task<string> GenerateAccessTokenAsync(ApplicationUser user);
        string GenerateRefreshToken();
    }

    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtSetting _settings;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SymmetricSecurityKey _signingKey;

        public JwtTokenService(IOptions<JwtSetting> settings, UserManager<ApplicationUser> userManager)
        {
            _settings = settings.Value;
            _userManager = userManager;
            _signingKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_settings.SecretKey));
        }

        public async Task<string> GenerateAccessTokenAsync(ApplicationUser user)
        {
            //pull roles form Identity
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,      user.Id),
                new Claim(JwtRegisteredClaimNames.Email,    user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,      Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Name,                  user.UserName)
            };

             /*
                Loops through each role
                Converts each role into a claim
                Adds them to the claims list
             */
            claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(_settings.ExpiresInMinutes),
                Issuer = _settings.Issuer,
                Audience = _settings.Audience,
                SigningCredentials = new SigningCredentials(_signingKey, SecurityAlgorithms.HmacSha256Signature)
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
        }

      

        public string GenerateRefreshToken()
        {
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Convert.ToBase64String(bytes);
        }
    }
}
