using FitPanel.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitPanel.Business.Services
{
    public interface IAuthService
    {
        Task<UserDto?> LoginAsync(LoginDto loginDto);
    }
}
