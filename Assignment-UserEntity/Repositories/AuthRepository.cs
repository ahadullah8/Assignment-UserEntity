using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.Models;
using Assignment_UserEntity.Repositories.Contrat;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;



namespace Assignment_UserEntity.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<User> _userManager;
        private readonly AppDbContext _context;
        public AuthRepository(UserManager<User> userManager, AppDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public async Task<bool> CheckPasswork(User user, string password)
        {
            return await _userManager.CheckPasswordAsync(user, password);
        }

        public Task<IdentityResult> CreateUser(User user, string password)
        {
            return _userManager.CreateAsync(user, password);
        }

        public async Task<User> GetUserByEmail(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }


        public async Task<bool> UserExists(string email, string userName)
        {
            var user = await _context.Users.Where(x => x.Email == email || x.UserName == userName).FirstOrDefaultAsync();
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}
