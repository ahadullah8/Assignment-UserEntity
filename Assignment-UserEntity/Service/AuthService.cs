using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.Models;
using Assignment_UserEntity.Service.Contract;
using Assignment_UserEntity.ServiceResponder;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assignment_UserEntity.Service
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly IMapper _mapper;

        public AuthService(UserManager<AppUser> userManager, IMapper mapper, IConfiguration configuration)
        {
            _userManager = userManager;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<ServiceResponse<string>> LoginAsync(LoginDto dto)
        {
            //get user by email
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return new ServiceResponse<string>()
                {
                    Data = string.Empty,
                    Success = false,
                    Message = "Invalid Credentials"
                };
            }
            var authClaims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("UserName",user.UserName)
            };
            var token = GetToken(authClaims);
            return new ServiceResponse<string>()
            {
                Data = token,
                Success = true,
                Message = "User loged in successfully"
            };
        }

        public async Task<ServiceResponse<string>> RegisterAsync(RegisterDto dto)
        {
            // check if email already exists
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                // create new app user
                var newUser = _mapper.Map<AppUser>(dto);
                newUser.SecurityStamp = Guid.NewGuid().ToString();
                var result = await _userManager.CreateAsync(newUser, dto.Password);
                if (!result.Succeeded)
                {
                    return new ServiceResponse<string>()
                    {
                        Data = string.Empty,
                        Success = false,
                        Message = "Unable to Register Try again!"
                    };
                }
                return new ServiceResponse<string>()
                {
                    Data = newUser.UserName + " Registered",
                    Success = true,
                    Message = "User registered successfully"
                };
            }
            return new ServiceResponse<string>()
            {
                Data = string.Empty,
                Success = false,
                Message = "User with this email already Exists"
            };
        }

        private string GetToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value));
            var expirationTime = DateTime.UtcNow.AddMinutes(30);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = expirationTime,
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                IssuedAt = DateTime.UtcNow,
                Issuer = _configuration.GetSection("JwtConfig:ValidIssuer").Value
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
