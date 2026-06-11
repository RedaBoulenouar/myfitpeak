namespace MyFittPeak.Api.Models;

public class Event
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public decimal Price { get; set; }

    public DateTimeOffset Date { get; set; }

    public int Capacity { get; set; }

    public Guid CoachId { get; set; }

    public CoachProfile Coach { get; set; } = null!;

    public ICollection<EventParticipant> Participants { get; set; } = new List<EventParticipant>();
}
