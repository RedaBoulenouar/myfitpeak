namespace MyFittPeak.Api.Contracts.Events;

public record CreateEventRequest(
    string Title,
    string Description,
    decimal Price,
    DateTimeOffset Date,
    int Capacity);
