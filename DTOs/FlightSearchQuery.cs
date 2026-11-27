using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace PRATATechnicalAssessment.DTOs;

public class FlightSearchQuery
{
    [FromQuery(Name = "from")]
    [Required(ErrorMessage = "The 'from' parameter is required.")]
    [RegularExpression(@"^[A-Za-z]{3}$", ErrorMessage = "'from' must be a 3-letter IATA code.")]
    public string From { get; set; } = string.Empty;

    [FromQuery(Name = "to")]
    [Required(ErrorMessage = "The 'to' parameter is required.")]
    [RegularExpression(@"^[A-Za-z]{3}$", ErrorMessage = "'to' must be a 3-letter IATA code.")]
    public string To { get; set; } = string.Empty;

    [FromQuery(Name = "date")]
    [Required(ErrorMessage = "The 'date' parameter is required (format: YYYY-MM-DD).")]
    public DateOnly? Date { get; set; }
}