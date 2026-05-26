using BudgetTracker.DTOs.Auth;
using Microsoft.IdentityModel.Tokens;
using System.CodeDom.Compiler;
using System.Collections.Concurrent;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace BudgetTracker.Services
{
    public record UserRecord(string Id, string Username, string Email, string PasswordHash);
    public interface IAuthService
    {
        Task<AuthResponseDto?> RegisterAsync(RegisterDto dto);
        Task<AuthResponseDto?> LoginAsync(LoginDto dto);
    }

    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;
        private readonly ConcurrentDictionary<string, UserRecord> _users = new();

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public Task<AuthResponseDto?> LoginAsync(LoginDto dto)
        {
            var user = _users.Values.FirstOrDefault(u => u.Email == dto.Email);

            if (user == null || VerifyPassword(dto.Password, user.PasswordHash))
                return Task.FromResult<AuthResponseDto?>(null);

            var response = GenerateToken(user);
            return Task.FromResult<AuthResponseDto?>(response);
        }

        public Task<AuthResponseDto?> RegisterAsync(RegisterDto dto)
        {
            if (_users.Values.Any(u => u.Email == dto.Email))
                return Task.FromResult<AuthResponseDto?>(null);

            var user = new UserRecord(
                Id: Guid.NewGuid().ToString(),
                Username: dto.Username,
                Email: dto.Email,
                PasswordHash: HashPassword(dto.Password)
            );

            _users[user.Id] = user;

            var response = GenerateToken(user);
            return Task.FromResult<AuthResponseDto?>(response);
        }

        //generate Token
        private AuthResponseDto GenerateToken(UserRecord user)
        {
            var jwtSettings = _config.GetSection("Jwt");
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expiry = DateTime.UtcNow.AddMinutes(
                                  double.Parse(jwtSettings["ExpiryMinutes"]!));

            //claims are data embeded inside the JWT token
            var claims = new[]
            {
               new Claim(JwtRegisteredClaimNames.Sub, user.Id),
               new Claim(JwtRegisteredClaimNames.Email, user.Email),
               new Claim(JwtRegisteredClaimNames.UniqueName, user.Username),
               new Claim(ClaimTypes.NameIdentifier, user.Id)
            };

            var token = new JwtSecurityToken(
                issuer: jwtSettings["Issuer"],
                audience: jwtSettings["Audience"],
                claims: claims,
                expires: expiry,
                signingCredentials: creds
            );

            return new AuthResponseDto
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token),
                Username = user.Username,
                ExpiresAt = expiry
            };
        }



        //always hash sensitive information
        private static string HashPassword(string password)
        {
            var bytes = SHA256.HashData(Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }

        private static bool VerifyPassword(string password, string hash)
            =>HashPassword(password) == hash;
    }
}
