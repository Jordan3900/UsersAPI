using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
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

        async public Task<User> AddUser(UserDТО userDТО)
        {
            var user = mapper.Map<UserDТО, User>(userDТО);

            var result = await userManager.CreateAsync(user, Constants.DEFAULT_PASSWORD);

            if (!result.Succeeded)
            {
                throw new Exception("Invalid User data");
            }

            return user;
        }

        async public Task<User> EditUser(string id, UserDТО userDТО)
        {
            var user = await userRepository
                .All()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (user == null)
            {
                throw new NullReferenceException();
            }

            user.FirstName = userDТО.FirstName;
            user.LastName = userDТО.LastName;

            await this.userRepository.SaveChangesAsync();

            return user;
        }

        async public Task DeleteUser(User user)
        {
            this.userRepository.Delete(user);

            await this.userRepository.SaveChangesAsync();
        }

        async public Task<IEnumerable<User>> GetAllUsers()
        {
            var users = await this.userRepository.All()
                .ToListAsync();

            return users;
        }

        async public Task<IEnumerable<UserDТО>> GetAllUsersDTO(int page, int perPage)
        {
            var users = await userRepository.All().Skip((page - 1) * perPage)
              .Take(perPage).ToListAsync();

            var usersDTO = mapper.Map<IEnumerable<User>, List<UserDТО>>(users);

            return usersDTO;
        }

        async public Task<User> GetUser(string id)
        {
            var user = await userRepository.All()
                .FirstOrDefaultAsync(x => x.Id == id);

            return user;
        }

        async public Task<UserDТО> GetUserDTO(string id)
        {
            var user = await userRepository.All().FirstOrDefaultAsync(x => x.Id == id);
            var userDto = mapper.Map<User, UserDТО>(user);

            return userDto;
        }
    }
}
