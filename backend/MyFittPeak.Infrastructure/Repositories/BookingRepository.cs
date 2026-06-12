using Microsoft.EntityFrameworkCore;
using MyFittPeak.Domain.Entities;
using MyFittPeak.Domain.Repositories;
using MyFittPeak.Infrastructure.Data;

namespace MyFittPeak.Infrastructure.Repositories;

public class BookingRepository : EfRepository<Booking>, IBookingRepository
{
    public BookingRepository(ApplicationDbContext dbContext)
        : base(dbContext)
    {
    }

    public async Task<IReadOnlyList<Booking>> ListForClientAsync(
        string clientId,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Bookings
            .Include(booking => booking.Coach)
            .ThenInclude(coach => coach.User)
            .Where(booking => booking.ClientId == clientId)
            .OrderByDescending(booking => booking.Date)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Booking>> ListForCoachAsync(
        Guid coachId,
        CancellationToken cancellationToken = default)
    {
        return await DbContext.Bookings
            .Include(booking => booking.Client)
            .Where(booking => booking.CoachId == coachId)
            .OrderByDescending(booking => booking.Date)
            .ToListAsync(cancellationToken);
    }
}
