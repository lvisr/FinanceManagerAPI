using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinanceManagerAPI.Data;
using FinanceManagerAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;


[Route("api/users")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _configuration;
    private readonly PasswordHasher<User> _passwordHasher;

    public UserController(AppDbContext context, IConfiguration configuration)
    {
        _context = context;
        _configuration = configuration;
        _passwordHasher = new PasswordHasher<User>();
    }

    // ✅ User Registration
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] User user)
    {
        if (await _context.Users.AnyAsync(u => u.Email == user.Email))
            return BadRequest("Email already exists.");

        user.PasswordHash = _passwordHasher.HashPassword(null, user.PasswordHash);
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return Ok(new { message = "User registered successfully" });
    }

    // ✅ User Login & Token Generation
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] User loginUser)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == loginUser.Email);
        if (user == null || _passwordHasher.VerifyHashedPassword(null, user.PasswordHash, loginUser.PasswordHash) == PasswordVerificationResult.Failed)
            return Unauthorized("Invalid email or password.");

        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }

    // ✅ Get User Profile
    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
            return NotFound("User not found.");

        return Ok(user);
    }

    // ✅ Update User Profile
    [HttpPut("profile")]
    [Authorize]
    public async Task<IActionResult> UpdateProfile([FromBody] User updatedUser)
    {
        var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
        var user = await _context.Users.FindAsync(userId);

        if (user == null)
            return NotFound("User not found.");

        user.Username = updatedUser.Username ?? user.Username;
        user.Email = updatedUser.Email ?? user.Email;

        if (!string.IsNullOrEmpty(updatedUser.PasswordHash))
        {
            user.PasswordHash = _passwordHasher.HashPassword(null, updatedUser.PasswordHash);
        }

        await _context.SaveChangesAsync();
        return Ok(new { message = "Profile updated successfully" });
    }

    private string GenerateJwtToken(User user)
    {
        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new Claim(ClaimTypes.Email, user.Email)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(6),
            signingCredentials: creds
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
