using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.Models;
using Microsoft.AspNetCore.Identity;

namespace Assignment_UserEntity.Repositories.Contrat
{
    public interface IAuthRepository
    {
        Task<User> GetUserByEmail(string email);
        Task<bool> HasPassword(User user);
        Task<bool> CheckPasswork(User user, string password);
        Task<IdentityResult> CreateUser(User user);
        Task<IdentityResult> AddPassword(User user, string password);

        Task<bool> UserExists(string email, string userName);
        Task<string> GenerateConfirmatioToken(User user);
        Task<IdentityResult> ConfirmEmailTokenAsync(User user, string token);

    }
}
