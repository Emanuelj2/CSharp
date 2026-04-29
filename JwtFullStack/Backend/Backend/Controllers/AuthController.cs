using Backend.Models;
using Backend.Models.DTOs;
using Backend.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.InteropServices;

namespace Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly IJwtTokenService _jwtTokenService;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthController(IJwtTokenService jwtTokenService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _jwtTokenService = jwtTokenService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        //register
        //
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto model)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEmail = await _userManager.FindByEmailAsync(model.Email);

            if (userEmail != null)
            {
                return BadRequest("Email is already in use.");
            }

            //create a new user
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                CreatedAt = DateTime.UtcNow
            };

            //create the user with password
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            //thinking of adding email conformation to prevent fake accounts

            return Ok("user registered successfully");
        }

        //login
        [HttpGet("login")]
        public async Task<IActionResult> Login()
        {
            return Ok();
        }
    }
}
