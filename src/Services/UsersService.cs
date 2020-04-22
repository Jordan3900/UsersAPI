using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersAPI.Data;
using UsersAPI.DtoModels;
using UsersAPI.Services.conracts;

namespace UsersAPI.Services
{
    public class UsersService : IUsersService
    {
        private ApplicationDbContext context;

        public UsersService(ApplicationDbContext context)
        {
            this.context = context;
        }


        public void EditUser(string id, UserDТО userDТО)
        {
            var user = context.Users.FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new NullReferenceException();
            }

            user.FirstName = userDТО.FirstName;
            user.LastName = userDТО.LastName;

            context.SaveChanges();
        }

        public IEnumerable<UserDТО> GetAllUsers()
        {
            var users = context.Users.Select(x => new UserDТО
            {
                FirstName = x.FirstName ?? "",
                LastName = x.LastName ?? "",
                Picture = x.Picture,
                Id = x.Id
            });

            return users;
        }

        public UserDТО GetUser(string id)
        {
            var user = context.Users.Select(x => new UserDТО
            {
                FirstName = x.FirstName ?? "",
                LastName = x.LastName ?? "",
                Picture = x.Picture,
                Id = x.Id
            }).FirstOrDefault(x => x.Id == id);

            return user;
        }
    }
}
