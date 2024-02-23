using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.Helpers;
using Assignment_UserEntity.Models;
using Assignment_UserEntity.Repositories.Contrat;
using Assignment_UserEntity.Services.Contract;
using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Security.Claims;
using System.Text;

namespace Assignment_UserEntity.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        private readonly IAuthRepository _authRepo;
        private readonly IEmailService _emailService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        //private HttpRequest _request;
        public AuthService(IMapper mapper,
            IConfiguration configuration,
            IAuthRepository authRepo,
            IEmailService emailService,
            IHttpContextAccessor httpContextAccessor)
        {
            _mapper = mapper;
            _configuration = configuration;
            _authRepo = authRepo;
            _emailService = emailService;
            _httpContextAccessor = httpContextAccessor;
            //_request = httpContextAccessor.HttpContext.Request;
        }

        public async Task<bool> ConfirEmailAsync(string token, string email)
        {
            var user = await _authRepo.GetUserByEmail(email);
            if (user != null)
            {
                if (user.EmailConfirmed)
                {
                    throw new Exception("This email is already varified");
                }
                var result = await _authRepo.ConfirmEmailTokenAsync(user, token);
                if (result.Succeeded)
                {
                    return true;
                }
                throw new Exception("Varification token is not valid");
            }
            throw new Exception("No user with this email exists");
        }
        public async Task<bool> SetPasswordAsync(string email, string password)
        {
            var user = await _authRepo.GetUserByEmail(email);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            else if (user.EmailConfirmed != true)
            {
                throw new Exception("Please confirm your email first");
            }
            else if (user.PasswordHash != null)
            {
                throw new Exception("User already has a passwrd");
            }
            var result = await _authRepo.AddPassword(user, password);
            if (!result.Succeeded)
            {
                throw new Exception(result.Errors.FirstOrDefault()?.ToString());
            }
            return true;
        }
        public async Task<string> LoginAsync(LoginDto dto)
        {
            //get user by email
            var user = await _authRepo.GetUserByEmail(dto.Email);
            if (user == null || !await _authRepo.CheckPasswork(user, dto.Password))
            {
                throw new Exception("Invalid Credentials");
            }
            // generate token and send it to the controller
            return GenerateJwtToken(JWTHealper.CreateClaims(user));
        }

        public async Task<UserDto> RegisterAsync(RegisterDto dto)
        {
            // check if email already exists
            if (!await _authRepo.UserExists(dto.Email, dto.UserName))
            {
                // create new app user
                var newUser = _mapper.Map<User>(dto);
                newUser.SecurityStamp = Guid.NewGuid().ToString();
                var result = await _authRepo.CreateUser(newUser);
                if (!result.Succeeded)
                {
                    throw new Exception("Unable to create user");
                }
                await SendConfirmationEmail(newUser);
                return _mapper.Map<UserDto>(newUser);
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

        private async Task SendConfirmationEmail(User user)
        {
            var token = await _authRepo.GenerateConfirmatioToken(user);
            Console.WriteLine(token);
            var request = _httpContextAccessor.HttpContext.Request;
            var host = (request.Host.Value ?? "localhost").Trim();
            var scheme = (request.Scheme ?? "http").Trim();

            var encodedToken = WebUtility.UrlEncode(token);
            var path = $"api/Auth/ConfirmEmail";
            var query = $"token={encodedToken}&email={user.Email}";
            Console.WriteLine(encodedToken);

            var url = new Uri($"{scheme}://{host}/{path}?{query}").ToString();
            var encodedUrl = WebUtility.HtmlEncode(url);

            Console.WriteLine(encodedUrl);
            //var link = @"https://localhost:7252/api/Auth/ConfirmEmail?replaceToken".Replace("replaceToken", JsonSerializer.Serialize(obj));
            var message = $"Please confirm your account by <a href='{encodedUrl}'>clicking here</a>.";

            await _emailService.SendEmailAsync(user.Email, "Confirm your email", message);
        }
    }
}
