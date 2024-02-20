using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.Middlewares.CustomAuthFilter;
using Assignment_UserEntity.Middlewares.Validator;
using Assignment_UserEntity.Services.Contract;
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
                return Ok(await _authService.RegisterAsync(registerDto));
                
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
                return Ok(await _authService.LoginAsync(loginDto));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
