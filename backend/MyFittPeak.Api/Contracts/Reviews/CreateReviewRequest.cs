namespace MyFittPeak.Api.Contracts.Reviews;

public record CreateReviewRequest(
    Guid CoachId,
    int Rating,
    string Comment);
