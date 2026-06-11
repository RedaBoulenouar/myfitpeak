using MyFittPeak.Domain.Entities;

namespace MyFittPeak.Domain.Repositories;

public interface ICoachProfileRepository : IRepository<CoachProfile>
{
    Task<IReadOnlyList<CoachProfile>> SearchAsync(
        string? sport,
        int? minimumRating,
        CancellationToken cancellationToken = default);
}
