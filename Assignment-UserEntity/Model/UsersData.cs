namespace Assignment_UserEntity.Model
{
    public class UsersData
    {
        public static List<User> UsersList = new List<User>()
        {
            new User()
            {
                Id = 1,
                FirstName = "Test",
                LastName = "1",
                Email = "Test1@gmail.com",
                UserName = "Test1",
            },
            new User()
            {
                Id = 2,
                FirstName = "Test",
                LastName = "2",
                Email = "Test2@gmail.com",
                UserName = "Test2",
            },
            new User()
            {
                Id = 3,
                FirstName = "Test",
                LastName = "3",
                Email = "Test3@gmail.com",
                UserName = "Test3",
            }
        };
    }
}
