using BCrypt.Net;
using FitPanel.Business.Helpers;
using FitPanel.Business.Services;
using FitPanel.DataAccess.Repositories;
using FitPanel.Entities.Concrete;
using FitPanel.Entities.Dtos;

namespace FitPanel.Business.Managers
{
    public class AuthService : IAuthService
    {
        private readonly IRepository<User> _userRepository;
        private readonly JwtTokenGenerator _tokenGenerator;

        public AuthService(IRepository<User> userRepository, JwtTokenGenerator tokenGenerator)
        {
            _userRepository = userRepository;
            _tokenGenerator = tokenGenerator;
        }

        public async Task<UserDto?> LoginAsync(LoginDto loginDto)
        {
            var users = await _userRepository.GetAllAsync();
            var user = users.FirstOrDefault(u => u.Email == loginDto.Email);
            if (user == null) return null;

            bool isPasswordValid = BCrypt.Net.BCrypt.Verify(loginDto.Password, user.PasswordHash);
            if (!isPasswordValid) return null;

            var token = _tokenGenerator.GenerateToken(user);

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                Token = token
            };
        }
    }

}
