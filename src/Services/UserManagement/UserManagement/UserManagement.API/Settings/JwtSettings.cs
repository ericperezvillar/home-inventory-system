using Microsoft.Extensions.Options;

namespace UserManagement.API.Settings
{
    public class JwtSettings : IOptions<JwtSettings>
    {
        public string Key { get; set; }

        public string Issuer { get; set; }

        public string Audience { get; set; }

        public JwtSettings Value => this;
    }
}
