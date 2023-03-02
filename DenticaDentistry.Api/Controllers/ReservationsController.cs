using Dentica_Dentistry.Application.Services;
using Dentica_Dentistry.Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Dentica_Dentistry.Api.Controllers;

[Route("reservations")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly IReservationsService _reservationService;

    public ReservationsController(IReservationsService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Reservation>> GetAllReservations() => Ok(_reservationService.GetAllReservations());

    [HttpGet("{id:int}")]
    public ActionResult<Reservation> GetReservation(int id)
    {
        var reservation = _reservationService.GetReservation(id);
        if (reservation is null)
        {
            return NotFound();
        }

        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult AddReservation(Reservation reservation)
    {
        var reservationId = _reservationService.CreateReservation(reservation);
        if (reservationId is null)
        {
            return BadRequest();
        }
        return CreatedAtAction(nameof(GetReservation), new { reservationId = reservation.ReservationId }, null);
    }

    [HttpPut("{id:int}")]
    public ActionResult ChangeReservationDate(int id, Reservation reservation)
    {
        reservation.ReservationId = id;
        if (_reservationService.UpdateReservationDate(id, reservation))
        {
            return NoContent();
        }

        return NotFound();
    }
    
    [HttpDelete("{id:int}")]
    public ActionResult DeleteReservation(int id)
    {
        if (_reservationService.DeleteReservation(id))
        {
            return NoContent();
        }

        return NotFound();
    }

}