using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Commands;
using DenticaDentistry.Application.DTO;
using DenticaDentistry.Application.Queries;
using DenticaDentistry.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Dentica_Dentistry.Api.Controllers;

[Route("reservations")]
[ApiController]
public class ReservationsController : ControllerBase
{
    private readonly ICommandHandler<CreateReservation> _createReservationHandler;
    private readonly ICommandHandler<DeleteReservation> _deleteReservationHandler;
    private readonly ICommandHandler<ChangeReservationDate> _changeReservationDateHandler;
    private readonly IQueryHandler<GetAllReservations, IEnumerable<ReservationDto>> _getAllReservationHandler;
    private readonly IQueryHandler<GetAllUserReservations, IEnumerable<ReservationDto>> _getAllUserReservationsHandler;
    private readonly IQueryHandler<GetFutureUserReservations, IEnumerable<ReservationDto>> _getFutureUserReservationsHandler;

    public ReservationsController(ICommandHandler<CreateReservation> createReservationHandler, ICommandHandler<DeleteReservation> deleteReservationHandler, ICommandHandler<ChangeReservationDate> changeReservationDateHandler, IQueryHandler<GetAllReservations, IEnumerable<ReservationDto>> getAllReservationHandler, IQueryHandler<GetAllUserReservations, IEnumerable<ReservationDto>> getAllUserReservationsHandler, IQueryHandler<GetFutureUserReservations, IEnumerable<ReservationDto>> getFutureUserReservationsHandler)
    {
        _createReservationHandler = createReservationHandler;
        _deleteReservationHandler = deleteReservationHandler;
        _changeReservationDateHandler = changeReservationDateHandler;
        _getAllReservationHandler = getAllReservationHandler;
        _getAllUserReservationsHandler = getAllUserReservationsHandler;
        _getFutureUserReservationsHandler = getFutureUserReservationsHandler;
    }
    
    [Authorize(Roles = "admin")]
    [HttpGet]
    [SwaggerOperation("Displaying all reservations")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status403Forbidden)]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAllReservations([FromQuery] GetAllReservations query) 
        => Ok(await _getAllReservationHandler.HandlerAsync(query));

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

    [Authorize]
    [HttpPost]
    [SwaggerOperation("Creating reservation for the service")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> AddReservation(CreateReservation command)
    {
        if (string.IsNullOrWhiteSpace(User.Identity?.Name))
        {
            return NotFound();
        }
        var userId = Guid.Parse(User.Identity?.Name);
        await _createReservationHandler.HandlerAsync(command with
        {
            ReservationId = Guid.NewGuid(),
            UserId = userId
        });
        return Ok();
    }

    [Authorize]
    [HttpPut("{reservationId:guid}")]
    [SwaggerOperation("Change reservation date")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> ChangeReservationDate(Guid reservationId, ChangeReservationDate command)
    {
        await _changeReservationDateHandler.HandlerAsync(command with { ReservationId = reservationId});
        return Ok();
    }

    [Authorize]
    [HttpDelete("{reservationId:guid}")]
    [SwaggerOperation("Delete reservation for the dentist service")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult> DeleteReservation(Guid reservationId)
    {
        await _deleteReservationHandler.HandlerAsync(new DeleteReservation(reservationId));
        return Ok();
    }

    [Authorize]
    [HttpGet]
    [Route("allUserReservations")]
    [SwaggerOperation("Get list of reservation by user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAllUserReservations([FromQuery] GetAllUserReservations query)
    {
        var userId = Guid.Parse(User.Identity?.Name);
        var reservations = await _getAllUserReservationsHandler.HandlerAsync(new GetAllUserReservations { UserId = userId });
        return Ok(reservations);
    }
    
    [Authorize]
    [HttpGet]
    [Route("futureUserReservations")]
    [SwaggerOperation("Get list of future reservation by user")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<IEnumerable<ReservationDto>>> GetFutureUserReservations([FromQuery] GetFutureUserReservations query)
    {
        var userId = Guid.Parse(User.Identity?.Name);
        var reservations = await _getFutureUserReservationsHandler.HandlerAsync(new GetFutureUserReservations { UserId = userId });
        return Ok(reservations);
    }
}