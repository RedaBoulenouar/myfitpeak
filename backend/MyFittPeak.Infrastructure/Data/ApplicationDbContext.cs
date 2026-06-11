using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MyFittPeak.Domain.Entities;

namespace MyFittPeak.Infrastructure.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<CoachProfile> CoachProfiles => Set<CoachProfile>();

    public DbSet<Event> Events => Set<Event>();

    public DbSet<EventParticipant> EventParticipants => Set<EventParticipant>();

    public DbSet<Booking> Bookings => Set<Booking>();

    public DbSet<Review> Reviews => Set<Review>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ApplicationUser>()
            .HasIndex(user => user.Role);

        builder.Entity<CoachProfile>()
            .HasOne(profile => profile.User)
            .WithOne(user => user.CoachProfile)
            .HasForeignKey<CoachProfile>(profile => profile.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<EventParticipant>()
            .HasKey(participant => new { participant.EventId, participant.ClientId });

        builder.Entity<EventParticipant>()
            .HasOne(participant => participant.Event)
            .WithMany(evt => evt.Participants)
            .HasForeignKey(participant => participant.EventId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<EventParticipant>()
            .HasOne(participant => participant.Client)
            .WithMany(user => user.EventParticipations)
            .HasForeignKey(participant => participant.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Booking>()
            .HasOne(booking => booking.Client)
            .WithMany(user => user.ClientBookings)
            .HasForeignKey(booking => booking.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Booking>()
            .HasOne(booking => booking.Coach)
            .WithMany(coach => coach.Bookings)
            .HasForeignKey(booking => booking.CoachId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Review>()
            .HasOne(review => review.Client)
            .WithMany(user => user.ReviewsWritten)
            .HasForeignKey(review => review.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Review>()
            .HasOne(review => review.Coach)
            .WithMany(coach => coach.Reviews)
            .HasForeignKey(review => review.CoachId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<CoachProfile>()
            .Property(profile => profile.HourlyRate)
            .HasPrecision(10, 2);

        builder.Entity<Event>()
            .Property(evt => evt.Price)
            .HasPrecision(10, 2);

        builder.Entity<Review>()
            .ToTable(table => table.HasCheckConstraint("CK_Reviews_Rating", "[Rating] BETWEEN 1 AND 5"));
    }
}

