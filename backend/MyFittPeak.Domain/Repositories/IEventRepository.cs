using MyFittPeak.Domain.Entities;

namespace MyFittPeak.Domain.Repositories;

public interface IEventRepository : IRepository<Event>
{
    Task<IReadOnlyList<Event>> ListUpcomingAsync(CancellationToken cancellationToken = default);

    Task<Event?> GetWithParticipantsAsync(Guid eventId, CancellationToken cancellationToken = default);

    Task<bool> IsClientRegisteredAsync(
        Guid eventId,
        string clientId,
        CancellationToken cancellationToken = default);
}
