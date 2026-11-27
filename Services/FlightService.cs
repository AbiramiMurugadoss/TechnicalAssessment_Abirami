using PRATATechnicalAssessment.DTOs;
using PRATATechnicalAssessment.Models;
using PRATATechnicalAssessment.Repositories;

namespace PRATATechnicalAssessment.Services;

public class FlightService : IFlightService
{
    private readonly IFlightRepository _repository;

    public FlightService(IFlightRepository repository)
    {
        _repository = repository;
    }

    public async Task<FlightResponse> CreateFlightAsync(
        CreateFlightRequest request,
        CancellationToken cancellationToken = default)
    {
        var source = request.Source.Trim().ToUpperInvariant();
        var destination = request.Destination.Trim().ToUpperInvariant();

        if (source == destination)
        {
            throw new ArgumentException("Source and destination airports cannot be the same.");
        }

        var departureUtc = NormalizeToUtc(request.DepartureTimeUtc);
        var arrivalUtc = NormalizeToUtc(request.ArrivalTimeUtc);

        if (arrivalUtc <= departureUtc)
        {
            throw new ArgumentException("Arrival time must be later than departure time.");
        }

        var flight = new Flight
        {
            AirlineName = request.AirlineName.Trim(),
            FlightNumber = request.FlightNumber.Trim().ToUpperInvariant(),
            Source = source,
            Destination = destination,
            DepartureTimeUtc = departureUtc,
            ArrivalTimeUtc = arrivalUtc,
            Price = decimal.Round(request.Price, 2)
        };

        var created = await _repository.AddAsync(flight, cancellationToken);
        return MapToResponse(created);
    }

    public async Task<FlightResponse?> GetFlightByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var flight = await _repository.GetByIdAsync(id, cancellationToken);
        return flight is null ? null : MapToResponse(flight);
    }

    public async Task<IReadOnlyList<FlightResponse>> SearchFlightsAsync(
        FlightSearchQuery query,
        CancellationToken cancellationToken = default)
    {
        var source = query.From.Trim().ToUpperInvariant();
        var destination = query.To.Trim().ToUpperInvariant();

        if (source == destination)
        {
            throw new ArgumentException("'from' and 'to' airport codes must be different.");
        }

        var date = query.Date!.Value;
        var dayStartUtc = DateTime.SpecifyKind(date.ToDateTime(TimeOnly.MinValue), DateTimeKind.Utc);
        var dayEndUtc = dayStartUtc.AddDays(1);

        var flights = await _repository.SearchAsync(
            source,
            destination,
            dayStartUtc,
            dayEndUtc,
            cancellationToken);

        return flights.Select(MapToResponse).ToList();
    }

    private static DateTime NormalizeToUtc(DateTime value)
    {
        return value.Kind switch
        {
            DateTimeKind.Utc => value,
            DateTimeKind.Local => value.ToUniversalTime(),
            _ => DateTime.SpecifyKind(value, DateTimeKind.Utc)
        };
    }

    private static FlightResponse MapToResponse(Flight flight) =>
        new(
            flight.Id,
            flight.AirlineName,
            flight.FlightNumber,
            flight.Source,
            flight.Destination,
            flight.DepartureTimeUtc,
            flight.ArrivalTimeUtc,
            flight.Price
        );
}