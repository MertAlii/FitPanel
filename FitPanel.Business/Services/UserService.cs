using FitPanel.Business.Services;
using FitPanel.DataAccess.Repositories;
using FitPanel.Entities.Concrete;
using FitPanel.Entities.Dtos;

namespace FitPanel.Business.Managers
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;

        public UserService(IRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<List<UserDto>> GetAllAsync()
        {
            var users = await _userRepository.GetAllAsync();
            return users.Select(u => new UserDto
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Gender = u.Gender,
                Email = u.Email
            }).ToList();
        }

        public async Task<UserDto?> GetByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            return new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Gender = user.Gender,
                Email = user.Email
            };
        }

        public async Task AddAsync(UserDto dto)
        {
            var passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            var user = new User
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                Email = dto.Email,
                PasswordHash = passwordHash,
                Role = "User" 
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();
        }


        public async Task UpdateAsync(UserDto dto)
        {
            var user = await _userRepository.GetByIdAsync(dto.Id);
            if (user == null) return;

            user.FirstName = dto.FirstName;
            user.LastName = dto.LastName;
            user.Gender = dto.Gender;
            user.Email = dto.Email;

            _userRepository.Update(user);
            await _userRepository.SaveAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return;

            _userRepository.Delete(user);
            await _userRepository.SaveAsync();
        }
    }
}
