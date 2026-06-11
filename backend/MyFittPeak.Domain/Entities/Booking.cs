namespace MyFittPeak.Domain.Entities;

public class Booking
{
    public Guid Id { get; set; }

    public string ClientId { get; set; } = string.Empty;

    public ApplicationUser Client { get; set; } = null!;

    public Guid CoachId { get; set; }

    public CoachProfile Coach { get; set; } = null!;

    public DateTimeOffset Date { get; set; }

    public string SessionType { get; set; } = "Online";

    public string PaymentStatus { get; set; } = "Pending";

    public string BookingStatus { get; set; } = "Requested";
}

