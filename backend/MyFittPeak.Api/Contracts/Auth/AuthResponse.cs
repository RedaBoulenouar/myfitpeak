namespace MyFittPeak.Api.Contracts.Auth;

public record AuthResponse(
    string AccessToken,
    DateTimeOffset ExpiresAt,
    string UserId,
    string Email,
    string Role);
