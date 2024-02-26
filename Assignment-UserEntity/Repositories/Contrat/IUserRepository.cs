using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.Models;

namespace Assignment_UserEntity.Repositories.Contrat
{
    public interface IUserRepository : IGenericRepository<User>
    {
        public bool UserExists(User newUser);

    }
}
