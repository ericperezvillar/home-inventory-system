using UserManagement.API.Entities;

namespace UserManagement.API.Repositories
{
    public interface IUserRepository
    {
        Task<User> AddAsync(User user);
        Task<User> GetByIdAsync(int id);
        Task<IEnumerable<User>> GetAllAsync();
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task<User?> GetByUsernameAsync(string username);
    }
}
