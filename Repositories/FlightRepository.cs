using Microsoft.EntityFrameworkCore;
using PRATATechnicalAssessment.Data;
using PRATATechnicalAssessment.Models;

namespace PRATATechnicalAssessment.Repositories;

public class FlightRepository : IFlightRepository
{
    private readonly FlightDbContext _context;

    public FlightRepository(FlightDbContext context)
    {
        _context = context;
    }

    public async Task<Flight> AddAsync(Flight flight, CancellationToken cancellationToken = default)
    {
        await _context.Flights.AddAsync(flight, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return flight;
    }

    public async Task<Flight?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        return await _context.Flights
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Flight>> SearchAsync(
        string source,
        string destination,
        DateTime dayStartUtc,
        DateTime dayEndUtc,
        CancellationToken cancellationToken = default)
    {
        var matchingFlights = await _context.Flights
            .AsNoTracking()
            .Where(f => f.Source == source
                     && f.Destination == destination
                     && f.DepartureTimeUtc >= dayStartUtc
                     && f.DepartureTimeUtc < dayEndUtc)
            .ToListAsync(cancellationToken);

        // Numeric sort in C# avoids SQLite text-column lexicographical ordering issues
        return matchingFlights
            .OrderBy(f => f.Price)
            .ThenBy(f => f.DepartureTimeUtc)
            .ToList();
    }
}