using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using MyFittPeak.Api.Constants;
using MyFittPeak.Api.Contracts.Auth;
using MyFittPeak.Api.Data;
using MyFittPeak.Api.Models;

namespace MyFittPeak.Api.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IConfiguration _configuration;
    private readonly UserManager<ApplicationUser> _userManager;

    public AuthController(
        ApplicationDbContext dbContext,
        IConfiguration configuration,
        UserManager<ApplicationUser> userManager)
    {
        _dbContext = dbContext;
        _configuration = configuration;
        _userManager = userManager;
    }

    [HttpPost("register")]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
    {
        var role = request.Role.Equals(AppRoles.Coach, StringComparison.OrdinalIgnoreCase)
            ? AppRoles.Coach
            : AppRoles.Client;

        var user = new ApplicationUser
        {
            UserName = request.Email,
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            Role = role
        };

        var result = await _userManager.CreateAsync(user, request.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result.Errors.Select(error => error.Description));
        }

        await _userManager.AddToRoleAsync(user, role);

        if (role == AppRoles.Coach)
        {
            _dbContext.CoachProfiles.Add(new CoachProfile
            {
                UserId = user.Id,
                Bio = string.Empty,
                SportsSpecialties = string.Empty,
                HourlyRate = 0
            });

            await _dbContext.SaveChangesAsync();
        }

        return Ok(CreateToken(user, role));
    }

    [HttpPost("login")]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        var user = await _userManager.FindByEmailAsync(request.Email);

        if (user is null || !await _userManager.CheckPasswordAsync(user, request.Password))
        {
            return Unauthorized();
        }

        var roles = await _userManager.GetRolesAsync(user);
        var role = roles.FirstOrDefault() ?? user.Role;

        return Ok(CreateToken(user, role));
    }

    private AuthResponse CreateToken(ApplicationUser user, string role)
    {
        var jwtSection = _configuration.GetSection("Jwt");
        var expiresAt = DateTimeOffset.UtcNow.AddHours(2);
        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSection["Key"]!));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Sub, user.Id),
            new(JwtRegisteredClaimNames.Email, user.Email!),
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!),
            new(ClaimTypes.Role, role)
        };

        var token = new JwtSecurityToken(
            issuer: jwtSection["Issuer"],
            audience: jwtSection["Audience"],
            claims: claims,
            expires: expiresAt.UtcDateTime,
            signingCredentials: credentials);

        return new AuthResponse(
            new JwtSecurityTokenHandler().WriteToken(token),
            expiresAt,
            user.Id,
            user.Email!,
            role);
    }
}
