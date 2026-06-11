namespace MyFittPeak.Api.Models;

public class EventParticipant
{
    public Guid EventId { get; set; }

    public Event Event { get; set; } = null!;

    public string ClientId { get; set; } = string.Empty;

    public ApplicationUser Client { get; set; } = null!;

    public DateTimeOffset RegisteredAt { get; set; } = DateTimeOffset.UtcNow;

    public string PaymentStatus { get; set; } = "Pending";
}
