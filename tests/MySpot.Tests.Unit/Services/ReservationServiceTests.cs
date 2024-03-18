using MySpot.Api.Commands;
using MySpot.Api.Entities;
using MySpot.Api.Repositories;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;
using MySpot.Tests.Unit.Shared;
using Shouldly;
using Xunit;

namespace MySpot.Tests.Unit.Services;

public class ReservationServiceTests
{

    [Fact]
    public void given_reservation_for_not_taken_date_create_reservation_should_succed()
    {

        // Arrange
        var parkingSpot = _weeklyParkingSpotRepository.GetAll().First();
        var command = new CreateReservation(parkingSpot.Id, Guid.NewGuid(), DateTime.UtcNow.AddMinutes(5), "John Doe", "XYZ123");

        // Act
        var reservationId = _reservationService.Create(command);

        // Assert
        reservationId.ShouldNotBeNull();
        reservationId.Value.ShouldBe(command.ReservationId);

    }


    #region Arrange

    private readonly IClock _clock;
    private readonly IReservationService _reservationService;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;

    public ReservationServiceTests()
    {
        _clock = new TestClock();
        _weeklyParkingSpotRepository = new InMemoryWeeklyParkingSpotRepository(_clock);
        _reservationService = new ReservationService(_clock, _weeklyParkingSpotRepository);
    }

    #endregion
}
