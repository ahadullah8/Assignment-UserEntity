using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.Models;
using Assignment_UserEntity.Repositories.Contrat;

namespace Assignment_UserEntity.Repositories
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public bool UserExists(User newUser)
        {
            var user = _context.Users.Where(x => x.UserName == newUser.UserName || x.Email == newUser.Email).FirstOrDefault();
            if (user == null)
            {
                return false;
            }
            return true;
        }
    }
}
