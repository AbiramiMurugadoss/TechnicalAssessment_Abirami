using Microsoft.EntityFrameworkCore;
using PRATATechnicalAssessment.Models;

namespace PRATATechnicalAssessment.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(FlightDbContext context)
    {
        if (await context.Flights.AnyAsync())
        {
            return;
        }

        var sampleDate = new DateTime(2026, 11, 15, 0, 0, 0, DateTimeKind.Utc);

        var flights = new List<Flight>
        {
            new()
            {
                AirlineName = "IndiGo",
                FlightNumber = "6E1751",
                Source = "MAA",
                Destination = "DXB",
                DepartureTimeUtc = sampleDate.AddHours(3).AddMinutes(30),
                ArrivalTimeUtc = sampleDate.AddHours(7).AddMinutes(10),
                Price = 14850.00m
            },
            new()
            {
                AirlineName = "Air India Express",
                FlightNumber = "IX389",
                Source = "MAA",
                Destination = "DXB",
                DepartureTimeUtc = sampleDate.AddHours(14).AddMinutes(0),
                ArrivalTimeUtc = sampleDate.AddHours(17).AddMinutes(35),
                Price = 9850.00m
            },
            new()
            {
                AirlineName = "Emirates",
                FlightNumber = "EK545",
                Source = "MAA",
                Destination = "DXB",
                DepartureTimeUtc = sampleDate.AddHours(9).AddMinutes(45),
                ArrivalTimeUtc = sampleDate.AddHours(13).AddMinutes(15),
                Price = 24600.00m
            },
            new()
            {
                AirlineName = "Flydubai",
                FlightNumber = "FZ8514",
                Source = "MAA",
                Destination = "DXB",
                DepartureTimeUtc = sampleDate.AddHours(21).AddMinutes(15),
                ArrivalTimeUtc = sampleDate.AddDays(1).AddHours(0).AddMinutes(50),
                Price = 16900.00m
            },
            new()
            {
                AirlineName = "IndiGo",
                FlightNumber = "6E1053",
                Source = "BLR",
                Destination = "SIN",
                DepartureTimeUtc = sampleDate.AddHours(6).AddMinutes(0),
                ArrivalTimeUtc = sampleDate.AddHours(12).AddMinutes(45),
                Price = 15400.00m
            }
        };

        await context.Flights.AddRangeAsync(flights);
        await context.SaveChangesAsync();
    }
}