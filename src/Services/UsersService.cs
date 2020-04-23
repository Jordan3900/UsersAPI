using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersAPI.Common;
using UsersAPI.Data.Contracts;
using UsersAPI.DtoModels;
using UsersAPI.Models;
using UsersAPI.Services.conracts;

namespace UsersAPI.Services
{
    public class UsersService : IUsersService
    {
        private readonly IRepository<User> userRepository;
        private readonly UserManager<User> userManager;

        public UsersService(IRepository<User> userRepository, UserManager<User> userManager)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
        }

        async public Task<bool> AddUser(UserDТО userDТО)
        {
            var user = new User
            {
                FirstName = userDТО.FirstName,
                LastName = userDТО.LastName,
                Picture = userDТО.Picture,
                Email = userDТО.Email,
                UserName = userDТО.Email
            };

            var result = await userManager.CreateAsync(user, Constants.DEFAULT_PASSWORD);

            return result.Succeeded;
        }

        public Task EditUser(string id, UserDТО userDТО)
        {
            var user = userRepository
                .All()
                .FirstOrDefault(x => x.Id == id);

            if (user == null)
            {
                throw new NullReferenceException();
            }

            user.FirstName = userDТО.FirstName;
            user.LastName = userDТО.LastName;

            return userRepository.SaveChangesAsync();
        }

        public void DeleteUser(User user)
        {
            this.userRepository.Delete(user);
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = this.userRepository.All();

            return users;
        }

        public IEnumerable<UserDТО> GetAllUsersDTO()
        {
            var users = userRepository.All().Select(x => new UserDТО
            {
                FirstName = x.FirstName ?? "",
                LastName = x.LastName ?? "",
                Picture = x.Picture,
                Id = x.Id
            });

            return users;
        }

        public User GetUser(string id)
        {
            var user = userRepository.All()
                .FirstOrDefault(x => x.Id == id);

            return user;
        }

        public UserDТО GetUserDTO(string id)
        {
            var user = userRepository.All().Select(x => new UserDТО
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
