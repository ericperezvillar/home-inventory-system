using UserManagement.API.DTOs;
using UserManagement.API.Entities;

namespace UserManagement.API.Interfaces
{
    public interface IUserService
    {
        Task<User> RegisterUser(UserDto userDto);
        Task<User?> AuthenticateUser(string username, string password);
        Task<User> GetUserById(int id);
        Task<IEnumerable<User>> GetAllUsers();
        Task UpdateUser(int id, UserDto userDto);
        Task DeleteUser(int id);
    }
}
