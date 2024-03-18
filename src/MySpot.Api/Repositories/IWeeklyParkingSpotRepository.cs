﻿using MySpot.Api.Entities;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Repositories;

public interface IWeeklyParkingSpotRepository
{
    WeeklyParkingSpot Get(ParkingSpotId id);
    IEnumerable<WeeklyParkingSpot> GetAll();
    void Add(WeeklyParkingSpot parkingSpot);
    void Update(WeeklyParkingSpot parkingSpot);
    void Delete(WeeklyParkingSpot parkingSpot);
}