namespace PRATATechnicalAssessment.Models;

public class Flight
{
    public int Id { get; set; }
    public string AirlineName { get; set; } = string.Empty;
    public string FlightNumber { get; set; } = string.Empty;
    public string Source { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime DepartureTimeUtc { get; set; }
    public DateTime ArrivalTimeUtc { get; set; }
    public decimal Price { get; set; }
}