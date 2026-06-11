using Microsoft.AspNetCore.Identity;

namespace MyFittPeak.Domain.Entities;

public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public string Role { get; set; } = MyFittPeak.Domain.Constants.AppRoles.Client;

    public CoachProfile? CoachProfile { get; set; }

    public ICollection<Booking> ClientBookings { get; set; } = new List<Booking>();

    public ICollection<Review> ReviewsWritten { get; set; } = new List<Review>();

    public ICollection<EventParticipant> EventParticipations { get; set; } = new List<EventParticipant>();
}

