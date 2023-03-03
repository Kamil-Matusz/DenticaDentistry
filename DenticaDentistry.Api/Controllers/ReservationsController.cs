using Dentica_Dentistry.Application.Commands;
using Dentica_Dentistry.Application.DTO;
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
    public ActionResult<IEnumerable<ReservationDto>> GetAllReservations() => Ok(_reservationService.GetAllReservations());

    [HttpGet("{id:guid}")]
    public ActionResult<ReservationDto> GetReservation(Guid id)
    {
        var reservation = _reservationService.GetReservation(id);
        if (reservation is null)
        {
            return NotFound();
        }

        return Ok(reservation);
    }

    [HttpPost]
    public ActionResult AddReservation(CreateReservation command)
    {
        var reservationId = _reservationService.CreateReservation(command with{ ReservationId = Guid.NewGuid()});
        if (reservationId is null)
        {
            return BadRequest();
        }
        
        
        return CreatedAtAction(nameof(GetReservation), new { reservationId}, null);
    }

    [HttpPut("{id:guid}")]
    public ActionResult ChangeReservationDate(Guid id, ChangeReservationDate command)
    {
        if (_reservationService.UpdateReservationDate(command with{ReservationId = id}))
        {
            return NoContent();
        }

        return NotFound();
    }
    
    [HttpDelete("{id:guid}")]
    public ActionResult DeleteReservation(Guid id)
    {
        if (_reservationService.DeleteReservation(new DeleteReservation(id)))
        {
            return NoContent();
        }

        return NotFound();
    }

}