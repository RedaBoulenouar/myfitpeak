using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFittPeak.Domain.Constants;
using MyFittPeak.Domain.Repositories;

namespace MyFittPeak.Api.Controllers;

[ApiController]
[Route("api/coaches")]
public class CoachProfilesController : ControllerBase
{
    private readonly ICoachProfileRepository _coachProfileRepository;

    public CoachProfilesController(ICoachProfileRepository coachProfileRepository)
    {
        _coachProfileRepository = coachProfileRepository;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> Search(
        [FromQuery] string? sport,
        [FromQuery] int? minRating,
        CancellationToken cancellationToken)
    {
        var coaches = await _coachProfileRepository.SearchAsync(sport, minRating, cancellationToken);

        var results = coaches
            .Select(coach => new
            {
                coach.Id,
                Name = coach.User.FirstName + " " + coach.User.LastName,
                coach.Bio,
                coach.SportsSpecialties,
                coach.HourlyRate,
                AverageRating = coach.Reviews.Any() ? coach.Reviews.Average(review => review.Rating) : 0
            })
            .ToList();

        return Ok(results);
    }

    [HttpPut("me")]
    [Authorize(Roles = AppRoles.Coach)]
    public IActionResult UpdateMyProfile()
    {
        return Accepted(new { message = "Coach profile update endpoint placeholder." });
    }
}
