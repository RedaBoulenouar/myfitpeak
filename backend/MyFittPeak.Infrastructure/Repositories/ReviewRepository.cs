using Microsoft.EntityFrameworkCore;
using MyFittPeak.Domain.Entities;
using MyFittPeak.Domain.Repositories;
using MyFittPeak.Infrastructure.Data;

namespace MyFittPeak.Infrastructure.Repositories;

public class ReviewRepository : EfRepository<Review>, IReviewRepository
{
    public ReviewRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<Review>> ListForCoachAsync(
        Guid coachId,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Reviews
            .Include(review => review.Client)
            .Where(review => review.CoachId == coachId)
            .OrderByDescending(review => review.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}
