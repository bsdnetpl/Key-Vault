using Key_Vault.Models;

namespace Key_Vault.Service
{
    public interface IUserService
    {
        Task<bool> AddUser(UserDto userDto);
    }
}