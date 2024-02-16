using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.ServiceResponder;

namespace Assignment_UserEntity.Service.Contract
{
    public interface IAuthService
    {
        Task<ServiceResponse<string>> LoginAsync(LoginDto dto);
        Task<ServiceResponse<string>> RegisterAsync(RegisterDto dto);
    }
}
