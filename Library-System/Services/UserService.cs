using AutoMapper;
using Library_System.Data.Dtos;
using Library_System.Models;
using Microsoft.AspNetCore.Identity;

namespace Library_System.Services
{
    public class UserService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<UserModel> _userManager;
        private readonly SignInManager<UserModel> _signInManager;
        private TokenServices _tokenServices;

        public UserService(IMapper mapper, UserManager<UserModel> userManager, SignInManager<UserModel> signInManager, TokenServices tokenService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenServices = tokenService;
        }

        public async Task Create(CreateUserDto dto)
        {
            UserModel user = _mapper.Map<UserModel>(dto);

            IdentityResult result = await _userManager.CreateAsync(user);

            if (!result.Succeeded) throw new ApplicationException("Falha ao cadastrar o usuario");
        }

        public async Task<string> Login(LoginUserDto dto)
        {
            var result = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);

            if(!result.Succeeded) throw new ApplicationException("Usuario não autenticado");

            var user = _signInManager
                .UserManager
                .Users
                .FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());

            var token = _tokenServices.GenerateToken(user);

            return token;
        }
    }
}
