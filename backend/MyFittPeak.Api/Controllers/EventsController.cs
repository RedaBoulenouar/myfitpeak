using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyFittPeak.Api.Contracts.Events;
using MyFittPeak.Domain.Constants;
using MyFittPeak.Domain.Entities;
using MyFittPeak.Domain.Repositories;

namespace MyFittPeak.Api.Controllers;

[ApiController]
[Route("api/events")]
public class EventsController : ControllerBase
{
    private readonly ICoachProfileRepository _coachProfiles;
    private readonly IEventRepository _events;
    private readonly IRepository<EventParticipant> _participants;
    private readonly IUnitOfWork _unitOfWork;

    public EventsController(
        ICoachProfileRepository coachProfiles,
        IEventRepository events,
        IRepository<EventParticipant> participants,
        IUnitOfWork unitOfWork)
    {
        _coachProfiles = coachProfiles;
        _events = events;
        _participants = participants;
        _unitOfWork = unitOfWork;
    }

    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> ListUpcoming(CancellationToken cancellationToken)
    {
        var events = await _events.ListUpcomingAsync(cancellationToken);

        return Ok(events.Select(evt => new
        {
            evt.Id,
            evt.Title,
            evt.Description,
            evt.Price,
            evt.Date,
            evt.Capacity,
            RemainingPlaces = Math.Max(0, evt.Capacity - evt.Participants.Count),
            CoachName = evt.Coach.User.FirstName + " " + evt.Coach.User.LastName
        }));
    }

    [HttpPost]
    [Authorize(Roles = AppRoles.Coach)]
    public async Task<IActionResult> Create(
        CreateEventRequest request,
        CancellationToken cancellationToken)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(userId))
        {
            return Unauthorized();
        }

        var coach = await _coachProfiles.GetByUserIdAsync(userId, cancellationToken);

        if (coach is null)
        {
            return NotFound("Coach profile not found.");
        }

        var evt = new Event
        {
            Title = request.Title,
            Description = request.Description,
            Price = request.Price,
            Date = request.Date,
            Capacity = request.Capacity,
            CoachId = coach.Id
        };

        await _events.AddAsync(evt, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CreatedAtAction(nameof(ListUpcoming), new { id = evt.Id }, new { evt.Id });
    }

    [HttpPost("{eventId:guid}/participants")]
    [Authorize(Roles = AppRoles.Client)]
    public async Task<IActionResult> Register(
        Guid eventId,
        CancellationToken cancellationToken)
    {
        var clientId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (string.IsNullOrWhiteSpace(clientId))
        {
            return Unauthorized();
        }

        var evt = await _events.GetWithParticipantsAsync(eventId, cancellationToken);

        if (evt is null)
        {
            return NotFound();
        }

        if (evt.Participants.Count >= evt.Capacity)
        {
            return Conflict("Event is full.");
        }

        if (await _events.IsClientRegisteredAsync(eventId, clientId, cancellationToken))
        {
            return Conflict("Client is already registered for this event.");
        }

        await _participants.AddAsync(new EventParticipant
        {
            EventId = eventId,
            ClientId = clientId,
            PaymentStatus = evt.Price > 0 ? "PendingStripeSimulation" : "Free"
        }, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Ok(new { eventId, paymentStatus = evt.Price > 0 ? "PendingStripeSimulation" : "Free" });
    }
}
