using Assignment_UserEntity.Constants;
using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.Helpers;
using Assignment_UserEntity.Models;
using Assignment_UserEntity.Repositories.Contrat;
using Assignment_UserEntity.Services.Contract;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Assignment_UserEntity.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepo;

        public AuthService(IMapper mapper, IConfiguration configuration, IAuthRepository authRepo)
        {
            _mapper = mapper;
            _configuration = configuration;
            _authRepo = authRepo;
        }

        public async Task<string> LoginAsync(LoginDto dto)
        {
            //get user by email
            var user = await _authRepo.GetUserByEmail(dto.Email);
            if (user == null || !await _authRepo.CheckPasswork(user,dto.Password))
            {
                throw new Exception("Invalid Credentials");
            }
            // generate token and send it to the controller
            return GenerateJwtToken(JWTHealper.CreateClaims(user));
        }

        public async Task<RegisterDto> RegisterAsync(RegisterDto dto)
        {
            // check if email already exists
            if (!await _authRepo.UserExists(dto.Email,dto.UserName))
            {
                // create new app user
                var newUser = _mapper.Map<User>(dto);
                newUser.SecurityStamp = Guid.NewGuid().ToString();
                var result = await _authRepo.CreateUser(newUser, dto.Password);
                if (!result.Succeeded)
                {
                    throw new Exception("Unable to create user");
                }
                dto.Password = null;
                return dto;
            }
            throw new Exception("User Already Exists");
        }

        private string GenerateJwtToken(List<Claim> claims)
        {
            int expTime = int.Parse(_configuration.GetSection(Constants.JwtConstants.JwtConfigExpiryTime).Value);
            var key = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_configuration.GetSection(Constants.JwtConstants.JwtConfigSecret).Value));
            var expirationTime = DateTime.UtcNow.AddMinutes(expTime);
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Expires = expirationTime,
                Subject = new ClaimsIdentity(claims),
                SigningCredentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256),
                IssuedAt = DateTime.UtcNow,
                Issuer = _configuration.GetSection(Constants.JwtConstants.JwtConfigValidIssuer).Value
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
