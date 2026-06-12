using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFittPeak.Api.Contracts.Bookings;
using MyFittPeak.Domain.Constants;
using MyFittPeak.Domain.Entities;
using MyFittPeak.Domain.Repositories;

namespace MyFittPeak.Api.Controllers;

[ApiController]
[Route("api/bookings")]
[Authorize]
public class BookingsController : ControllerBase
{
    private readonly IBookingRepository _bookings;
    private readonly ICoachProfileRepository _coachProfiles;
    private readonly IUnitOfWork _unitOfWork;

    public BookingsController(
        IBookingRepository bookings,
        ICoachProfileRepository coachProfiles,
        IUnitOfWork unitOfWork)
    {
        _bookings = bookings;
        _coachProfiles = coachProfiles;
        _unitOfWork = unitOfWork;
    }

    [HttpGet("me")]
    public async Task<IActionResult> ListMine(CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        if (User.IsInRole(AppRoles.Coach))
        {
            var coach = await _coachProfiles.GetByUserIdAsync(userId, cancellationToken);

            if (coach is null)
            {
                return NotFound("Coach profile not found.");
            }

            var coachBookings = await _bookings.ListForCoachAsync(coach.Id, cancellationToken);

            return Ok(coachBookings.Select(booking => new
            {
                booking.Id,
                booking.Date,
                booking.SessionType,
                booking.PaymentStatus,
                booking.BookingStatus,
                ClientName = booking.Client.FirstName + " " + booking.Client.LastName
            }));
        }

        var clientBookings = await _bookings.ListForClientAsync(userId, cancellationToken);

        return Ok(clientBookings.Select(booking => new
        {
            booking.Id,
            booking.Date,
            booking.SessionType,
            booking.PaymentStatus,
            booking.BookingStatus,
            CoachName = booking.Coach.User.FirstName + " " + booking.Coach.User.LastName
        }));
    }

    [HttpPost]
    [Authorize(Roles = AppRoles.Client)]
    public async Task<IActionResult> Create(
        CreateBookingRequest request,
        CancellationToken cancellationToken)
    {
        var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(clientId))
        {
            return Unauthorized();
        }

        var booking = new Booking
        {
            ClientId = clientId,
            CoachId = request.CoachId,
            Date = request.Date,
            SessionType = string.IsNullOrWhiteSpace(request.SessionType) ? "Online" : request.SessionType,
            PaymentStatus = "PendingStripeSimulation",
            BookingStatus = "Requested"
        };

        await _bookings.AddAsync(booking, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(ListMine), new { id = booking.Id }, new { booking.Id });
    }
}
