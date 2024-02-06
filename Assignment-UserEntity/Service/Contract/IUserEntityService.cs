using Assignment_UserEntity.Dtos;
using Assignment_UserEntity.ServiceResponder;

namespace Assignment_UserEntity.Service.Contract
{
    public interface IUserEntityService
    {
        /// <summary>
        /// returns details of a user
        /// </summary>
        /// <param name="id">identifier of the user</param>
        /// <returns>detials of a user</returns>
        ServiceResponse<UserDto> GetUser(int id);

        /// <summary>
        /// takes a user and add it to existing list of users
        /// </summary>
        /// <param name="newUser">Data of new user</param>
        /// <returns></returns>
        ServiceResponse<UserDto> AddUser(UserDto? newUser);

        /// <summary>
        /// gets user by id and update the content of the already existing user
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUser"></param>
        /// <returns></returns>
        ServiceResponse<UserDto> UpdateUser(int id, UserDto updateUser);

        /// <summary>
        /// deletes the user whoes id is given
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ServiceResponse<string> DeleteUser(int id);


    }
}
