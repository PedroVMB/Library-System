using AutoMapper;
using Library_System.Data.Dtos;
using Library_System.Models;

namespace Library_System.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDto, UserModel>();
        }
    }
}
