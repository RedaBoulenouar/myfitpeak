using MyFittPeak.Domain.Entities;

namespace MyFittPeak.Domain.Repositories;

public interface IReviewRepository : IRepository<Review>
{
    Task<IReadOnlyList<Review>> ListForCoachAsync(
        Guid coachId,
        CancellationToken cancellationToken = default);
}
