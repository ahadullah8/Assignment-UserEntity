using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.Middlewares.CustomAuthFilter;
using Assignment_UserEntity.Middlewares.Validator;
using Assignment_UserEntity.Services.Contract;
using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Reflection.Metadata.Ecma335;

namespace Assignment_UserEntity.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;
        private readonly IEmailService _emailService;

        public AuthController(IAuthService authService, IEmailService emailService)
        {
            _authService = authService;
            _emailService = emailService;
        }

        [HttpPost]
        [ValidateModelState]
        [Route("Register")]
        [CustomAuthFilter]
        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            try
            {
                var result = await _authService.RegisterAsync(registerDto);

                return Ok(result);

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

        [HttpGet]
        [Route("ConfirmEmail")]
        [ValidateModelState]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            try
            {
                var result = await _authService.ConfirEmailAsync(token, email);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("SetPassword")]
        [ValidateModelState]
        public async Task<IActionResult> SetPassword(LoginDto dto)
        {
            try
            {
                return Ok(await _authService.SetPasswordAsync(dto.Email, dto.Password));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
