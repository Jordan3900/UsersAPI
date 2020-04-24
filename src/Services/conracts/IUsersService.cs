using System.Collections.Generic;
using System.Threading.Tasks;
using UsersAPI.DtoModels;
using UsersAPI.Models;

namespace UsersAPI.Services.Conracts
{
    public interface IUsersService
    {
        IEnumerable<User> GetAllUsers();

        IEnumerable<UserDТО> GetAllUsersDTO();

        User GetUser(string id);

        UserDТО GetUserDTO(string id);

        Task EditUser(string id, UserDТО userDТО);

        Task<bool> AddUser(UserDТО userDТО);

        Task DeleteUser(User user);
    }
}
