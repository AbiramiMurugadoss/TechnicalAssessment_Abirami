using PRATATechnicalAssessment.DTOs;

namespace PRATATechnicalAssessment.Services;

public interface IFlightService
{
    Task<FlightResponse> CreateFlightAsync(CreateFlightRequest request, CancellationToken cancellationToken = default);
    Task<FlightResponse?> GetFlightByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<FlightResponse>> SearchFlightsAsync(FlightSearchQuery query, CancellationToken cancellationToken = default);
}