using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsersAPI.Common;
using UsersAPI.Data.Contracts;
using UsersAPI.DtoModels;
using UsersAPI.Models;
using UsersAPI.Services.Conracts;

namespace UsersAPI.Services
{
    public class UsersService : IUsersService
    {
        private readonly IRepository<User> userRepository;
        private readonly UserManager<User> userManager;
        private readonly IMapper mapper;
        public UsersService(IRepository<User> userRepository,
            UserManager<User> userManager,
            IMapper mapper)
        {
            this.userManager = userManager;
            this.userRepository = userRepository;
            this.mapper = mapper;
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

            return this.userRepository.SaveChangesAsync();
        }

        public Task DeleteUser(User user)
        {
            this.userRepository.Delete(user);

            return this.userRepository.SaveChangesAsync();
        }

        public IEnumerable<User> GetAllUsers()
        {
            var users = this.userRepository.All();

            return users;
        }

        public IEnumerable<UserDТО> GetAllUsersDTO()
        {
            var users = userRepository.All();
            var usersDTO = mapper.Map<IQueryable<User>, List<UserDТО>>(users);

            return usersDTO;
        }

        public User GetUser(string id)
        {
            var user = userRepository.All()
                .FirstOrDefault(x => x.Id == id);

            return user;
        }

        public UserDТО GetUserDTO(string id)
        {
            var user = userRepository.All().FirstOrDefault(x => x.Id == id);
            var userDto = mapper.Map<User, UserDТО>(user);
            return userDto;
        }
    }
}
