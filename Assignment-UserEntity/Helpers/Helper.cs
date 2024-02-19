using Assignment_UserEntity.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Assignment_UserEntity.Helpers
{
    public class Helper
    {
        public static List<Claim> CreateClaims(AppUser user)
        {
            return new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.Email),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("UserName",user.UserName)
            };
        }
    }
}
