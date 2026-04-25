using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace RoleClaimsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // "api/user"
    public class UserController : Controller
    {

        [HttpGet("role-based")]
        public IActionResult GetUserByRole()
        {
            //simulate a logged in user with a role
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "John Doe"),
                new Claim(ClaimTypes.Role, "Admin")
            }, "mock"));


            HttpContext.User = user;

            if (user.IsInRole("Admin"))
            {
                return Ok(new { Message = "Hello Admin!" });
            }
            else
            {
                return Forbid();
            }
        }


        [HttpGet("claim-based")]
        public IActionResult GetUserByCliaim()
        {
            //simulate a logged in user with a role
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, "John Doe"),
                new Claim("Department", "IT")
            }, "mock"));


            HttpContext.User = user;

            var hasClaim = user.HasClaim(c => c.Type == "Department" && c.Value == "IT");

            if (hasClaim)
            {
                return Ok(new { Message = "Access Grnted to the IT department." });
            }
            else
            {
                return Forbid();
            }
        }


    }
}
