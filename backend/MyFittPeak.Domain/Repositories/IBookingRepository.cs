using MyFittPeak.Domain.Entities;

namespace MyFittPeak.Domain.Repositories;

public interface IBookingRepository : IRepository<Booking>
{
    Task<IReadOnlyList<Booking>> ListForClientAsync(
        string clientId,
        CancellationToken cancellationToken = default);

    Task<IReadOnlyList<Booking>> ListForCoachAsync(
        Guid coachId,
        CancellationToken cancellationToken = default);
}
