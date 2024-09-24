using Microsoft.AspNetCore.Identity;
using System;
using UserManagement.API.DTOs;
using UserManagement.API.Entities;
using UserManagement.API.Interfaces;
using UserManagement.API.Repositories;

namespace UserManagement.API.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly PasswordHasher<User> _passwordHasher;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
            _passwordHasher = new PasswordHasher<User>();
        }

        public async Task<User> RegisterUser(UserDto userDto)
        {
            var passwordHash = HashPassword(userDto.Password);
            var user = new User
            {
                Username = userDto.Username,
                Email = userDto.Email,
                PasswordHash = passwordHash,
                FirstName = userDto.FirstName,
                LastName = userDto.LastName,
                CreatedAt = DateTime.UtcNow,
                UpdatedAt = DateTime.UtcNow
            };

            return await _userRepository.AddAsync(user);
        }

        public async Task<User?> AuthenticateUser(string username, string password)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            if (user == null || !VerifyPassword(password, user.PasswordHash))
            {
                return null; // Authentication failed
            }
            return user; // Authentication successful
        }

        public async Task<User> GetUserById(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await _userRepository.GetAllAsync();
        }

        public async Task UpdateUser(int id, UserDto userDto)
        {
            var user = await GetUserById(id);
            if (user == null) throw new Exception("User not found");

            //user.Username = userDto.Username;
            user.Email = userDto.Email;
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.UpdatedAt = DateTime.UtcNow;

            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUser(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        private string HashPassword(string password)
        {
            var hashedPassword = _passwordHasher.HashPassword(null, password);
            return hashedPassword;
        }

        private bool VerifyPassword(string password, string hashedPassword)
        {
            var result = _passwordHasher.VerifyHashedPassword(null, hashedPassword, password);
            return result == PasswordVerificationResult.Success;
        }
    }

}
