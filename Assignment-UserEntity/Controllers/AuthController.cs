using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.Helpers.CustomAuthFilter;
using Assignment_UserEntity.Helpers.Validator;
using Assignment_UserEntity.Service.Contract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace Assignment_UserEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        [ValidateModelState]
        [Route("Register")]
        [CustomAuthFilter]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                var res = await _authService.RegisterAsync(registerDto);
                if (res.Success)
                {
                    return Ok(res.Data);
                }
                return BadRequest(res.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [ValidateModelState]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            try
            {
                var res = await _authService.LoginAsync(loginDto);
                if (res.Success)
                {
                    return Ok(res.Data);
                }
                return BadRequest(res.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
