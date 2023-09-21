using Key_Vault.DB;
using Key_Vault.Models;
using System.Data.SqlTypes;

namespace Key_Vault.Service
{

    public class UserService : IUserService
    {
        private readonly DbConnection _connection;

        public UserService(DbConnection connection)
        {
            _connection = connection;
        }
        public async Task<bool> AddUser(UserDto userDto)
        {
            User user = new User();
            user.dateTimeCreate = DateTime.Now;
            user.password = userDto.password;
            user.name = userDto.name;
            user.email = userDto.email;
            await _connection.users.AddAsync(user);
            await _connection.SaveChangesAsync();
            return true;
        }

    }
}
