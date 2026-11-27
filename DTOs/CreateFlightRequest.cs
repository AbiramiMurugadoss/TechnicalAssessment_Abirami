using System.ComponentModel.DataAnnotations;

namespace PRATATechnicalAssessment.DTOs;

public class CreateFlightRequest
{
    [Required]
    [StringLength(100, MinimumLength = 2)]
    public string AirlineName { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^[A-Z0-9]{2,3}-?\d{1,4}$", ErrorMessage = "FlightNumber must be a valid code such as EK545 or 6E-1751.")]
    public string FlightNumber { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^[A-Za-z]{3}$", ErrorMessage = "Source must be a 3-letter IATA airport code (e.g., MAA).")]
    public string Source { get; set; } = string.Empty;

    [Required]
    [RegularExpression(@"^[A-Za-z]{3}$", ErrorMessage = "Destination must be a 3-letter IATA airport code (e.g., DXB).")]
    public string Destination { get; set; } = string.Empty;

    [Required]
    public DateTime DepartureTimeUtc { get; set; }

    [Required]
    public DateTime ArrivalTimeUtc { get; set; }

    [Required]
    [Range(0.01, 10000000, ErrorMessage = "Price must be greater than zero.")]
    public decimal Price { get; set; }
}