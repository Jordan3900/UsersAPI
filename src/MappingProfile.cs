using AutoMapper;
using UsersAPI.Models;
using UsersAPI.DtoModels;

namespace UsersAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserDТО>();
        }
    }
}
