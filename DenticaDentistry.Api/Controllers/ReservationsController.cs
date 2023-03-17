using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Commands;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using Microsoft.AspNetCore.Mvc;

namespace Dentica_Dentistry.Api.Controllers;

[Route("reservations")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly ICommandHandler<CreateReservation> _createReservationHandler;
    private readonly ICommandHandler<DeleteReservation> _deleteReservationHandler;
    private readonly ICommandHandler<ChangeReservationDate> _changeReservationDateHandler;
    private readonly IQueryHandler<GetAllReservations, IEnumerable<ReservationDto>> _getAllReservationHandler;

    public ReservationsController(ICommandHandler<CreateReservation> createReservationHandler, ICommandHandler<DeleteReservation> deleteReservationHandler, ICommandHandler<ChangeReservationDate> changeReservationDateHandler, IQueryHandler<GetAllReservations, IEnumerable<ReservationDto>> getAllReservationHandler)
    {
        _createReservationHandler = createReservationHandler;
        _deleteReservationHandler = deleteReservationHandler;
        _changeReservationDateHandler = changeReservationDateHandler;
        _getAllReservationHandler = getAllReservationHandler;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAllReservations([FromQuery] GetAllReservations query) => Ok(await _getAllReservationHandler.HandlerAsync(query));

    /*[HttpGet("{id:guid}")]
    public async Task<ActionResult<ReservationDto>> GetReservation(Guid id)
    {
        var reservation = await _reservationService.GetReservationAsync(id);
        if (reservation is null)
        {
            return NotFound();
        }

        return Ok(reservation);
    }*/

    [HttpPost]
    public async Task<ActionResult> AddReservation(int dentistIndustryId,CreateReservation command)
    {
        await _createReservationHandler.HandlerAsync(command with
        {
            ReservationId = Guid.NewGuid(),
            DentistIndustryId = dentistIndustryId,
        });
        return NoContent();
    }

    [HttpPut("{reservationId:guid}")]
    public async Task<ActionResult> ChangeReservationDate(Guid reservationId, ChangeReservationDate command)
    {
        await _changeReservationDateHandler.HandlerAsync(command with { ReservationId = reservationId});
        return NoContent();
    }
    
    [HttpDelete("{reservationId:guid}")]
    public async Task<ActionResult> DeleteReservation(Guid reservationId)
    {
        await _deleteReservationHandler.HandlerAsync(new DeleteReservation(reservationId));
        return NoContent();
    }

}