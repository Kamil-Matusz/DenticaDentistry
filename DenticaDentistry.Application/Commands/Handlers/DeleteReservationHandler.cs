using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Exceptions;
using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.Repositories;

namespace DenticaDentistry.Application.Commands.Handlers;

public sealed class DeleteReservationHandler : ICommandHandler<DeleteReservation>
{
    private readonly IReservationRepository _reservationRepository;

    public DeleteReservationHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task HandlerAsync(DeleteReservation command)
    {
        var reservationId = await GetReservationsAsync(command.ReservationId);
        if (reservationId is null)
        {
            throw new DentistIndustryIdNotFoundException();
        }
        
        reservationId.RemoveReservation(command.ReservationId);
        await _reservationRepository.DeleteAsync(reservationId);
    }
    
    private async Task<DentistIndustry> GetReservationsAsync(Guid reservationId)
    {
        var reservations = await _reservationRepository.GetAllReservationAsync();
            
        return reservations.SingleOrDefault(x => x.Reservations.Any(r => r.ReservationId == reservationId));
    }
}