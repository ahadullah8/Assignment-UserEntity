using Assignment_UserEntity.Dtos;

namespace Assignment_UserEntity.Services.Contract
{
    public interface IAuthService
    {
        Task<string> LoginAsync(LoginDto dto);
        Task<RegisterDto> RegisterAsync(RegisterDto dto);
    }
}
