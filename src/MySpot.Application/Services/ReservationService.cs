﻿using MySpot.Application.Commands;
using MySpot.Application.DTO;
using MySpot.Application.Services;
using MySpot.Core.Entities;
using MySpot.Core.Repositories;
using MySpot.Core.ValueObjects;

namespace MySpot.Application.Services;

public class ReservationService : IReservationService
{
    private readonly IClock _clock;
    private readonly IWeeklyParkingSpotRepository _weeklyParkingSpotRepository;


    public ReservationService(IClock clock, IWeeklyParkingSpotRepository weeklyParkingSpotRepository)
    {
        _clock = clock;
        _weeklyParkingSpotRepository = weeklyParkingSpotRepository;
    }

    public ReservationDto Get(Guid id) 
        => GetAllWeekly().SingleOrDefault(x => x.Id == id);

    public IEnumerable<ReservationDto> GetAllWeekly() 
        => _weeklyParkingSpotRepository.GetAll().SelectMany(x=> x.Reservations).Select(x=> new ReservationDto
        {
            Id = x.Id,
            ParkingSpotId = x.ParkingSpotId,
            EmployeeName = x.EmployeeName,
            Date = x.Date.Value.Date
        });

    public Guid? Create(CreateReservation command)
    {
        var parkingSpotId = new ParkingSpotId(command.ParkingSpotId);
        var weeklyParkingSpot = _weeklyParkingSpotRepository.Get(parkingSpotId);
        if (weeklyParkingSpot is null)
        {
            return default;
        }

        var reservation = new Reservation(command.ReservationId, command.ParkingSpotId, command.EmployeeName, command.LicensePlate, new Date(command.Date));
        weeklyParkingSpot.AddReservation(reservation, new Date(_clock.Current()));

        return reservation.Id;

    }

    public bool Update(ChangeReservationLicensePlate command)
    {

        var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
        if(weeklyParkingSpot is null)
        {
            return false;
        }

        var reservationId = new ReservationId(command.ReservationId);
        var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == reservationId);
        if (existingReservation is null) return false;

        if(existingReservation.Date.Value.Date <= _clock.Current())
        {
            return false;
        }

        existingReservation.ChangeLicensePlate(command.LicensePlate);

        return true;
    }

    public bool Delete(DeleteReservation command)
    {

        var weeklyParkingSpot = GetWeeklyParkingSpotByReservation(command.ReservationId);
        if (weeklyParkingSpot is null)
        {
            return false;
        }

        var reservationId = new ReservationId(command.ReservationId);
        var existingReservation = weeklyParkingSpot.Reservations.SingleOrDefault(x => x.Id == reservationId);
        if (existingReservation is null) return false;

        weeklyParkingSpot.RemoveReservation(existingReservation);

        return true;
    }

    private WeeklyParkingSpot GetWeeklyParkingSpotByReservation(ReservationId reservationId)
        => _weeklyParkingSpotRepository.GetAll().SingleOrDefault(x => x.Reservations.Any(x => x.Id == reservationId));
}
