using BudgetTracker.DTOs.Auth;
using BudgetTracker.Services;
using Microsoft.AspNetCore.Mvc;

namespace BudgetTracker.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        //ID
        public AuthController (IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var result = await _authService.RegisterAsync(dto);
            if (result is null)
                return Conflict(new { message = "Email is already registered" });
            return CreatedAtAction(nameof(Register), result);
        }

        [HttpPost("loggin")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            var result = await _authService.LoginAsync(dto);
            if (result is null)
                return Conflict(new { message = "Invalid email or password" });
            return Ok(result);
        }


    }
}
