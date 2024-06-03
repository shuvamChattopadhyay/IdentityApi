using IdentityExample.Models;
using Microsoft.AspNetCore.Identity;

namespace IdentityExample.Services
{
    public interface IUserServices
    {
        Task<ResponseModel> CreateUserAsync(UserDTO userData);
        Task<ResponseModel> GetUserList();
        Task<ResponseModel> Login(LoginRequest login);
        Task<string> GenerateTokenString(LoginRequest login, IdentityUser user);
        Task<ResponseModel> CreateRoles(string roleName);
    }
}