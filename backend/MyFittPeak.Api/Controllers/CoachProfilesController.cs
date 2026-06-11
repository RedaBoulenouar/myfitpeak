using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyFittPeak.Api.Constants;
using MyFittPeak.Api.Data;

namespace MyFittPeak.Api.Controllers;

[ApiController]
[Route("api/coaches")]
public class CoachProfilesController : ControllerBase
{
    private readonly ApplicationDbContext _dbContext;

    public CoachProfilesController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Search([FromQuery] string? sport, [FromQuery] int? minRating)
    {
        var coaches = _dbContext.CoachProfiles
            .Include(coach => coach.User)
            .Include(coach => coach.Reviews)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(sport))
        {
            coaches = coaches.Where(coach => coach.SportsSpecialties.Contains(sport));
        }

        var results = await coaches
            .Select(coach => new
            {
                coach.Id,
                Name = coach.User.FirstName + " " + coach.User.LastName,
                coach.Bio,
                coach.SportsSpecialties,
                coach.HourlyRate,
                AverageRating = coach.Reviews.Any() ? coach.Reviews.Average(review => review.Rating) : 0
            })
            .Where(coach => !minRating.HasValue || coach.AverageRating >= minRating.Value)
            .ToListAsync();

        return Ok(results);
    }

    [HttpPut("me")]
    [Authorize(Roles = AppRoles.Coach)]
    public IActionResult UpdateMyProfile()
    {
        return Accepted(new { message = "Coach profile update endpoint placeholder." });
    }
}
