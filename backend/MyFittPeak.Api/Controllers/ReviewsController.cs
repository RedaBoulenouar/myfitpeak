using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFittPeak.Api.Contracts.Reviews;
using MyFittPeak.Domain.Constants;
using MyFittPeak.Domain.Entities;
using MyFittPeak.Domain.Repositories;

namespace MyFittPeak.Api.Controllers;

[ApiController]
[Route("api/reviews")]
public class ReviewsController : ControllerBase
{
    private readonly IReviewRepository _reviews;
    private readonly IUnitOfWork _unitOfWork;

    public ReviewsController(IReviewRepository reviews, IUnitOfWork unitOfWork)
    {
        _reviews = reviews;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("coach/{coachId:guid}")]
    [AllowAnonymous]
    public async Task<IActionResult> ListForCoach(
        Guid coachId,
        CancellationToken cancellationToken)
    {
        var reviews = await _reviews.ListForCoachAsync(coachId, cancellationToken);

        return Ok(reviews.Select(review => new
        {
            review.Id,
            review.Rating,
            review.Comment,
            review.CreatedAt,
            ClientName = review.Client.FirstName + " " + review.Client.LastName
        }));
    }

    [HttpPost]
    [Authorize(Roles = AppRoles.Client)]
    public async Task<IActionResult> Create(
        CreateReviewRequest request,
        CancellationToken cancellationToken)
    {
        var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(clientId))
        {
            return Unauthorized();
        }

        if (request.Rating is < 1 or > 5)
        {
            return BadRequest("Rating must be between 1 and 5.");
        }

        var review = new Review
        {
            ClientId = clientId,
            CoachId = request.CoachId,
            Rating = request.Rating,
            Comment = request.Comment
        };

        await _reviews.AddAsync(review, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(
            nameof(ListForCoach),
            new { coachId = review.CoachId },
            new { review.Id });
    }
}
