using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MusicTheory.Domain;
using MusicTheory.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Asp.Versioning;

namespace MusicTheory.Api.Controllers;

/// <summary>
/// Controller for user authentication (registration and login)
/// </summary>
[ApiVersion(0)]
[ApiController]
[Route("api/v{v:apiVersion}/[controller]")]
public class AuthController : ControllerBase
{
    // Identity takes care of hashing the password and storing the user record in the database.
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IConfiguration _configuration;

    /// <summary>
    /// Constructor for AuthController
    /// </summary>
    /// <param name="userManager">UserManager instance for managing users</param>
    /// <param name="configuration">Configuration instance for accessing app settings</param>
    public AuthController(
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration)
    {
        _userManager = userManager;
        _configuration = configuration;
    }

    /// <summary>
    /// Register a new user
    /// </summary>
    /// <param name="model">Registration model containing user details</param>
    /// <returns>Returns the result of the registration process</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     curl --location 'https://musictheoryapi.com/api/v0/auth/register' \
    ///     --header 'Content-Type: application/json' \
    ///     --data-raw '{
    ///         "Email": "user@example.com",
    ///         "Password": "MyStrongPassword123",
    ///     }'
    ///
    /// </remarks>
    /// <response code="201">The user was created successfully</response>
    /// <response code="400">Bad Request</response>
    // POST: api/v0/Auth/register
    [MapToApiVersion(0)]
    [HttpPost("register")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [AllowAnonymous] // Allow public access to registration
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Create a new user instance
        var user = new ApplicationUser
        {
            UserName = model.Email, // Should these both be using model.Email?
            Email = model.Email
        };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (result.Succeeded) return Created(); // 201 Created
        // Return validation errors (e.g., password too weak or email already taken)
        var errors = string.Join("; ", result.Errors.Select(e => e.Description));
        return BadRequest(new { message = errors }); // 400 Bad Request
    }

    /// <summary>
    /// Get a JWT token for the user
    /// </summary>
    /// <param name="model">Login model containing user credentials</param>
    /// <returns>If successful, returns a JWT token</returns>
    /// <remarks>
    /// Sample request:
    ///
    ///     curl --location 'https://musictheoryapi.com/api/v0/auth/login' \
    ///     --header 'Content-Type: application/json' \
    ///     --data-raw '{
    ///         "Email": "user@example.com",
    ///         "Password": "MyStrongPassword123",
    ///     }'
    ///
    /// Successful response:
    ///
    ///     {
    ///         "token": "xyz123abc456"
    ///     }
    ///
    /// </remarks>
    /// <response code="200">Token generated successfully</response>
    /// <response code="401">Not authorized; invalid credentials</response>
    // POST: api/v0/Auth/login
    [MapToApiVersion(0)]
    [HttpPost("login")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [AllowAnonymous]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        // Find the user by email and verify the password

        // Find the user by email (or username)
        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null)
        {
            return Unauthorized(new { message = "Invalid credentials" });
        }

        // Check the password
        // CheckPasswordAsync internally hashes the provided password and compares it to the stored hash
        bool passwordOk = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!passwordOk)
        {
            return Unauthorized(new { message = "Invalid credentials" });
        }

        // Create JWT token for the user
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? string.Empty); // secret key from config
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity([
                new Claim(JwtRegisteredClaimNames.Sub, user.Id),
                new Claim(JwtRegisteredClaimNames.Email, user.Email!),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()) // unique token ID
            ]),
            Expires = DateTime.UtcNow.AddMinutes(
                int.Parse(_configuration["JWT:ExpiryMinutes"] ?? string.Empty)),
            Issuer = _configuration["JWT:Issuer"],
            Audience = _configuration["JWT:Audience"],
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var securityToken = tokenHandler.CreateToken(tokenDescriptor);
        string tokenString = tokenHandler.WriteToken(securityToken);

        return Ok(new { token = tokenString });
    }
}
