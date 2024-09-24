using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserManagement.API.Entities;
using UserManagement.API.Interfaces;
using UserManagement.API.Settings;

namespace UserManagement.API.Helpers
{
    public class JwtTokenHelper : IJwtTokenHelper
    {
        private readonly JwtSettings _jwtSettings;
        private readonly ILogger<JwtTokenHelper> _logger;

        public JwtTokenHelper(IOptions<JwtSettings> jwtSetting, ILogger<JwtTokenHelper> logger)
        {
            _logger = logger;
            _jwtSettings = jwtSetting.Value;
        }

        public string GenerateToken(User user)
        {
            try
            {
                var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
                var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

                var tokenHandler = new JwtSecurityTokenHandler();
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.Name, user.Username),
                        new Claim(ClaimTypes.Email, user.Email)
                    }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    Issuer = _jwtSettings.Issuer,
                    Audience = _jwtSettings.Audience,
                    SigningCredentials = signinCredentials
                };

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error generating token for user {user.Email}");
                throw;
            }            
        }
    }
}
