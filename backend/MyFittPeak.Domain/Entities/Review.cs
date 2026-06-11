namespace MyFittPeak.Domain.Entities;

public class Review
{
    public Guid Id { get; set; }

    public int Rating { get; set; }

    public string Comment { get; set; } = string.Empty;

    public string ClientId { get; set; } = string.Empty;

    public ApplicationUser Client { get; set; } = null!;

    public Guid CoachId { get; set; }

    public CoachProfile Coach { get; set; } = null!;

    public DateTimeOffset CreatedAt { get; set; } = DateTimeOffset.UtcNow;
}

