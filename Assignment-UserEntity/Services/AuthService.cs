using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.Helpers;
using Assignment_UserEntity.Models;
using Assignment_UserEntity.Services.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assignment_UserEntity.Services
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

        public async Task<string> LoginAsync(LoginDto dto)
        {
            //get user by email
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null || !await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                throw new Exception("Invalid Credentials");
            }
            // generate token and send it to the controller
            return GenerateToken(Helper.CreateClaims(user));
        }

        public async Task<RegisterDto> RegisterAsync(RegisterDto dto)
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
                    throw new Exception("Unable to create user");
                }
                return dto;
            }
            throw new Exception("User Already Exists");
        }

        private string GenerateToken(List<Claim> claims)
        {
            int expTime = int.Parse(_configuration.GetSection(ApiConsts.JwtConfigExpiryTime).Value);
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection(ApiConsts.JwtConfigSecret).Value));
            var expirationTime = DateTime.UtcNow.AddMinutes(expTime);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = expirationTime,
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                IssuedAt = DateTime.UtcNow,
                Issuer = _configuration.GetSection(ApiConsts.JwtConfigValidIssuer).Value
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
