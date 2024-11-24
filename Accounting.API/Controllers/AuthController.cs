using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Accounting.API.DAOs.Person;
using Accounting.API.DTOs.Person;
using Accounting.API.Services.Person.PasswordHasher;

namespace Accounting.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController(IPasswordHasher passwordHasher) : Controller
    {
        private readonly IPasswordHasher _passwordHasher = passwordHasher;

        [HttpPost]
        [Route("credentials")]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        [ProducesResponseType(StatusCodes.Status403Forbidden)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult SetCredentials(PersonCredentialsDto personCredentials)
        {
            if (personCredentials.PersonID <= 0) return BadRequest();

            var res = _passwordHasher.HashPassword(personCredentials.Password);
            //await _personService.StoreCredentials(personCredentials.PersonID, res);
            return Ok();
        }

        [HttpPost("login")]
        public IActionResult Login(string username, string password)
        {
            // TODO: Validate the username and password against your database or any other authentication mechanism.
            // If the validation succeeds, proceed to generate and return a JWT token.

            var userClaims = new[]
            {
                new Claim(ClaimTypes.Name, username),
                new Claim(ClaimTypes.Role, "admin") // Example claim for an admin user
            };

            //var token = GenerateJwtToken(userClaims);

            return Ok(new { });
        }

        [HttpGet("protected-resource")]
        [Authorize]
        public IActionResult ProtectedResource()
        {
            // This endpoint is protected with authorization.
            // Only authenticated users with valid JWT tokens can access it.
            // You can access the authenticated user's claims here.

            //var username = User.Identity.Name;

            // TODO: Return the protected resource data

            return Ok();
        }

        //private string GenerateJwtToken(Claim[] claims)
        //{
        //    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        //    var signingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        //    var tokenOptions = new JwtSecurityToken(
        //        issuer: _configuration["Jwt:Issuer"],
        //        audience: _configuration["Jwt:Audience"],
        //        claims: claims,
        //        expires: DateTime.UtcNow.AddHours(1), // Set token expiration time as needed
        //        signingCredentials: signingCredentials
        //    );

        //    var token = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        //    return token;
        //}
    }
}

