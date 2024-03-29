﻿using MySpot.Core.Entities;
using MySpot.Application.Services;
using MySpot.Core.ValueObjects;
using MySpot.Core.Repositories;

namespace MySpot.Infrastructure.Repositories;

internal class InMemoryWeeklyParkingSpotRepository : IWeeklyParkingSpotRepository
{
    private readonly IClock _clock;
    private readonly List<WeeklyParkingSpot> _weeklyParkingSpots;

    public InMemoryWeeklyParkingSpotRepository(IClock clock)
    {
        _clock = clock;

        _weeklyParkingSpots = new List<WeeklyParkingSpot>()
        {
            new (Guid.Parse("00000000-0000-0000-0000-000000000001"), new Week(clock.Current()), "P1"),
            new (Guid.Parse("00000000-0000-0000-0000-000000000002"), new Week(clock.Current()), "P2"),
            new (Guid.Parse("00000000-0000-0000-0000-000000000003"), new Week(clock.Current()), "P3"),
            new (Guid.Parse("00000000-0000-0000-0000-000000000004"), new Week(clock.Current()), "P4"),
            new (Guid.Parse("00000000-0000-0000-0000-000000000005"), new Week(clock.Current()), "P5")
        };
}

    public WeeklyParkingSpot Get(ParkingSpotId id) => _weeklyParkingSpots.SingleOrDefault(x=> x.Id == id);

    public IEnumerable<WeeklyParkingSpot> GetAll() => _weeklyParkingSpots;

    public void Add(WeeklyParkingSpot parkingSpot) => _weeklyParkingSpots.Add(parkingSpot);

    public void Update(WeeklyParkingSpot parkingSpot)
    {
        
    }

    public void Delete(WeeklyParkingSpot parkingSpot) => _weeklyParkingSpots.Remove(parkingSpot);
}
