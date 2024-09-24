using UserManagement.API.Entities;

namespace UserManagement.API.Interfaces
{
    public interface IJwtTokenHelper
    {
        string GenerateToken(User user);
    }
}
