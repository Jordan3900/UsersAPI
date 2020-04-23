using System.Collections.Generic;
using System.Threading.Tasks;
using UsersAPI.DtoModels;
using UsersAPI.Models;

namespace UsersAPI.Services.conracts
{
    public interface IUsersService
    {
        IEnumerable<User> GetAllUsers();

        IEnumerable<UserDТО> GetAllUsersDTO();

        User GetUser(string id);

        UserDТО GetUserDTO(string id);

        Task EditUser(string id, UserDТО userDТО);

        Task<bool> AddUser(UserDТО userDТО);

        void DeleteUser(User user);
    }
}
