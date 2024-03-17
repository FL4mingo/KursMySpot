using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;
using Shouldly;
using Xunit;

namespace MySpot.Tests.Unit.Services;

public class ReservationServiceTests
{

    [Fact]
    public void given_reservation_for_not_taken_date_create_reservation_should_succed()
    {

        // Arrange
        var parkingSpot = _weeklyParkingSpots.First();
        var command = new CreateReservation(parkingSpot.Id, Guid.NewGuid(), DateTime.UtcNow.AddMinutes(5), "John Doe", "XYZ123");

        // Act
        var reservationId = _reservationService.Create(command);

        // Assert
        reservationId.ShouldNotBeNull();
        reservationId.Value.ShouldBe(command.ReservationId);

    }


    #region Arrange

    private readonly ReservationService _reservationService;
    private readonly Clock _clock = new();
    private readonly List<WeeklyParkingSpot> _weeklyParkingSpots;

    public ReservationServiceTests()
    {
        _weeklyParkingSpots = new List<WeeklyParkingSpot>()
        {
            new (Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(_clock.Current()),  "P1"),
            new (Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(_clock.Current()), "P2"),
            new (Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(_clock.Current()), "P3"),
            new (Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(_clock.Current()), "P4"),
            new (Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(_clock.Current()), "P5")
        };

        _reservationService = new ReservationService(_weeklyParkingSpots);
    }

    #endregion
}
