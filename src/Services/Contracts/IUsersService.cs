using System.Collections.Generic;
using System.Threading.Tasks;
using UsersAPI.DtoModels;
using UsersAPI.Models;

namespace UsersAPI.Services.Contracts
{
    public interface IUsersService
    {
        Task<IEnumerable<User>> GetAllUsers();

        Task<IEnumerable<UserDТО>> GetAllUsersDTO(int page, int perPage);

        Task<User> GetUser(string id);

        Task<UserDТО> GetUserDTO(string id);

        Task<User> EditUser(string id, UserDТО userDТО);

        Task<User> AddUser(UserDТО userDТО);

        Task DeleteUser(User user);
    }
}
