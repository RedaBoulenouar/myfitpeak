namespace MyFittPeak.Api.Contracts.Coaches;

public record UpdateCoachProfileRequest(
    string Bio,
    string SportsSpecialties,
    decimal HourlyRate,
    string AvailabilityJson);
