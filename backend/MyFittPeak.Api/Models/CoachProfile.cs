namespace MyFittPeak.Api.Models;

public class CoachProfile
{
    public Guid Id { get; set; }

    public string UserId { get; set; } = string.Empty;

    public ApplicationUser User { get; set; } = null!;

    public string Bio { get; set; } = string.Empty;

    public string SportsSpecialties { get; set; } = string.Empty;

    public decimal HourlyRate { get; set; }

    public string AvailabilityJson { get; set; } = "{}";

    public ICollection<Event> Events { get; set; } = new List<Event>();

    public ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}
