namespace PRATATechnicalAssessment.DTOs;

public record FlightResponse(
    int Id,
    string AirlineName,
    string FlightNumber,
    string Source,
    string Destination,
    DateTime DepartureTimeUtc,
    DateTime ArrivalTimeUtc,
    decimal Price
);