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
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAllReservations() => Ok(await _reservationService.GetAllReservationsAsync());

    [HttpGet("{id:guid}")]
    public async Task<ActionResult<ReservationDto>> GetReservation(Guid id)
    {
        var reservation = await _reservationService.GetReservationAsync(id);
        if (reservation is null)
        {
            return NotFound();
        }

        return Ok(reservation);
    }

    [HttpPost]
    public async Task<ActionResult> AddReservation(CreateReservation command)
    {
        var reservationId = await _reservationService.CreateReservationAsync(command with{ ReservationId = Guid.NewGuid()});
        if (reservationId is null)
        {
            return BadRequest();
        }
        
        return CreatedAtAction(nameof(GetReservation), new { reservationId}, null);
    }

    [HttpPut("{id:guid}")]
    public async Task<ActionResult> ChangeReservationDate(Guid id, ChangeReservationDate command)
    {
        if (await _reservationService.UpdateReservationDateAsync(command with{ReservationId = id}))
        {
            return NoContent();
        }

        return NotFound();
    }
    
    [HttpDelete("{id:guid}")]
    public async Task<ActionResult> DeleteReservation(Guid id)
    {
        if (await _reservationService.DeleteReservationAsync(new DeleteReservation(id)))
        {
            return NoContent();
        }

        return NotFound();
    }

}