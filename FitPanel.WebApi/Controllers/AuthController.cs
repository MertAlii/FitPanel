using Microsoft.AspNetCore.Mvc;
using FitPanel.Business.Services;
using FitPanel.Entities.Dtos;

namespace FitPanel.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            var token = await _authService.LoginAsync(loginDto);
            if (token == null)
                return Unauthorized("Email veya şifre hatalı.");

            return Ok(new { token });
        }

    }
}
