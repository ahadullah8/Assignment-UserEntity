using Assignment_UserEntity.Dtos;

namespace Assignment_UserEntity.Services.Contract
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDto dto);
        Task<UserDto> RegisterAsync(RegisterDto dto);
        Task<bool> ConfirEmailAsync(string token, string email);
        Task<bool> SetPasswordAsync(string email, string password);
    }
}
