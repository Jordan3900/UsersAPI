using System.Collections.Generic;
using UsersAPI.DtoModels;

namespace UsersAPI.Services.conracts
{
    public interface IUsersService
    {
        IEnumerable<UserDТО> GetAllUsers();

        UserDТО GetUser(string id);


        void EditUser(string id, UserDТО userDТО);
    }
}
