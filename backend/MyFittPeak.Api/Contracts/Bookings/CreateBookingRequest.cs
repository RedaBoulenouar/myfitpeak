namespace MyFittPeak.Api.Contracts.Bookings;

public record CreateBookingRequest(
    Guid CoachId,
    DateTimeOffset Date,
    string SessionType);
