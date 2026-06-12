using Microsoft.EntityFrameworkCore;
using MyFittPeak.Domain.Entities;
using MyFittPeak.Domain.Repositories;
using MyFittPeak.Infrastructure.Data;

namespace MyFittPeak.Infrastructure.Repositories;

public class EventRepository : EfRepository<Event>, IEventRepository
{
    public EventRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<Event>> ListUpcomingAsync(CancellationToken cancellationToken = default)
    {
        return await DbContext.Events
            .Include(evt => evt.Coach)
            .ThenInclude(coach => coach.User)
            .Include(evt => evt.Participants)
            .Where(evt => evt.Date >= DateTimeOffset.UtcNow)
            .OrderBy(evt => evt.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task<Event?> GetWithParticipantsAsync(
        Guid eventId,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Events
            .Include(evt => evt.Participants)
            .FirstOrDefaultAsync(evt => evt.Id == eventId, cancellationToken);
    }

    public async Task<bool> IsClientRegisteredAsync(
        Guid eventId,
        string clientId,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.EventParticipants
            .AnyAsync(participant =>
                participant.EventId == eventId &&
                participant.ClientId == clientId,
                cancellationToken);
    }
}
