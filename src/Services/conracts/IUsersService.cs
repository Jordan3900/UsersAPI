using System.Collections.Generic;
using System.Threading.Tasks;
using UsersAPI.DtoModels;
using UsersAPI.Models;

namespace UsersAPI.Services.Conracts
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<IEnumerable<UserDТО>> GetAllUsersDTO(int page, int perPage);

        Task<User> GetUser(string id);

        Task<UserDТО> GetUserDTO(string id);

        Task EditUser(string id, UserDТО userDТО);

        Task<bool> AddUser(UserDТО userDТО);

        Task DeleteUser(User user);
    }
}
