using Microsoft.EntityFrameworkCore;
using MyFittPeak.Domain.Entities;
using MyFittPeak.Domain.Repositories;
using MyFittPeak.Infrastructure.Data;

namespace MyFittPeak.Infrastructure.Repositories;

public class CoachProfileRepository : EfRepository<CoachProfile>, ICoachProfileRepository
{
    public CoachProfileRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<CoachProfile?> GetByUserIdAsync(
        string userId,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.CoachProfiles
            .Include(coach => coach.User)
            .Include(coach => coach.Reviews)
            .FirstOrDefaultAsync(coach => coach.UserId == userId, cancellationToken);
    }

    public async Task<IReadOnlyList<CoachProfile>> SearchAsync(
        string? sport,
        int? minimumRating,
        CancellationToken cancellationToken = default)
    {
        var query = DbContext.CoachProfiles
            .Include(coach => coach.User)
            .Include(coach => coach.Reviews)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(sport))
        {
            query = query.Where(coach => coach.SportsSpecialties.Contains(sport));
        }

        if (minimumRating.HasValue)
        {
            query = query.Where(coach =>
                coach.Reviews.Any() &&
                coach.Reviews.Average(review => review.Rating) >= minimumRating.Value);
        }

        return await query.ToListAsync(cancellationToken);
    }
}
