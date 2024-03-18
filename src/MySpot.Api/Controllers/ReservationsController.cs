using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MySpot.Api.Commands;
using MySpot.Api.DTO;
using MySpot.Api.Entities;
using MySpot.Api.Services;
using MySpot.Api.ValueObjects;

namespace MySpot.Api.Controllers;

[Route("reservations")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IReservationService _reservationService;

    public ReservationsController(IReservationService reservationService)
    {
        
        _reservationService = reservationService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<ReservationDto>> Get() => Ok(_reservationService.GetAllWeekly());

    [HttpGet("{id:Guid}")]
    public ActionResult<ReservationDto> Get(Guid id)
    {
        var reservation = _reservationService.Get(id);
        if(reservation is null) return NotFound();

        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult Post(CreateReservation command)
    {

        var id = _reservationService.Create(command with {ReservationId = Guid.NewGuid() });
        if(id is null) return BadRequest();

        return CreatedAtAction(nameof(Get), new {id}, null);

    }

    [HttpPut("{id:Guid}")]
    public ActionResult Put(Guid id, ChangeReservationLicensePlate command)
    {

        if(_reservationService.Update(command with { ReservationId = id})) return NoContent();

        return NotFound();
    }

    [HttpDelete("{id:Guid}")]
    public ActionResult Delete(Guid id)
    {

        if (_reservationService.Delete(new DeleteReservation(id))) return NoContent();

        return NotFound();
    }
}
