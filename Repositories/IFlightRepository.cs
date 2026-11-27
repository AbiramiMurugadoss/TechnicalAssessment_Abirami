using PRATATechnicalAssessment.Models;

namespace PRATATechnicalAssessment.Repositories;

public interface IFlightRepository
{
    Task<Flight> AddAsync(Flight flight, CancellationToken cancellationToken = default);
    Task<Flight?> GetByIdAsync(int id, CancellationToken cancellationToken = default);
    Task<IReadOnlyList<Flight>> SearchAsync(
        string source,
        string destination,
        DateTime dayStartUtc,
        DateTime dayEndUtc,
        CancellationToken cancellationToken = default);
}