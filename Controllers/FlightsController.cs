using Microsoft.AspNetCore.Mvc;
using PRATATechnicalAssessment.DTOs;
using PRATATechnicalAssessment.Services;

namespace PRATATechnicalAssessment.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class FlightsController : ControllerBase
{
    private readonly IFlightService _flightService;

    public FlightsController(IFlightService flightService)
    {
        _flightService = flightService;
    }

    /// <summary>
    /// Creates a new flight schedule entry.
    /// </summary>
    [HttpPost]
    [ProducesResponseType(typeof(FlightResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<FlightResponse>> CreateFlight(
        [FromBody] CreateFlightRequest request,
        CancellationToken cancellationToken)
    {
        var created = await _flightService.CreateFlightAsync(request, cancellationToken);
        return CreatedAtAction(nameof(GetFlightById), new { id = created.Id }, created);
    }

    /// <summary>
    /// Retrieves a single flight by ID.
    /// </summary>
    [HttpGet("{id:int}")]
    [ProducesResponseType(typeof(FlightResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<FlightResponse>> GetFlightById(
        int id,
        CancellationToken cancellationToken)
    {
        var flight = await _flightService.GetFlightByIdAsync(id, cancellationToken);
        if (flight is null)
        {
            return NotFound(new ProblemDetails
            {
                Status = StatusCodes.Status404NotFound,
                Title = "Flight Not Found",
                Detail = $"No flight found with ID {id}."
            });
        }

        return Ok(flight);
    }

    /// <summary>
    /// Searches flights by origin, destination, and travel date, sorted by lowest price.
    /// </summary>
    /// <remarks>Example: GET /api/flights/search?from=MAA&amp;to=DXB&amp;date=2026-11-15</remarks>
    [HttpGet("search")]
    [ProducesResponseType(typeof(IReadOnlyList<FlightResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ValidationProblemDetails), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IReadOnlyList<FlightResponse>>> SearchFlights(
        [FromQuery] FlightSearchQuery query,
        CancellationToken cancellationToken)
    {
        var results = await _flightService.SearchFlightsAsync(query, cancellationToken);
        return Ok(results);
    }
}