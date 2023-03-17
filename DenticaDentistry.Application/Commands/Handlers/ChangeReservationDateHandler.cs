using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Exceptions;
using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.Repositories;

namespace DenticaDentistry.Application.Commands.Handlers;

public sealed class ChangeReservationDateHandler : ICommandHandler<ChangeReservationDate>
{
    private readonly IReservationRepository _reservationRepository;

    public ChangeReservationDateHandler(IReservationRepository reservationRepository)
    {
        _reservationRepository = reservationRepository;
    }

    public async Task HandlerAsync(ChangeReservationDate command)
    {
        var dentistIndustry = await GetReservationsAsync(command.ReservationId);
        if (dentistIndustry is null)
        {
            throw new DentistIndustryIdNotFoundException();
        }

        var reservationId = command.ReservationId;
        var existingReservation = dentistIndustry.Reservations.SingleOrDefault(x => x.ReservationId == reservationId);
        if (existingReservation is null)
        {
            throw new ReservationNotFoundException(command.ReservationId);
        }
        
        existingReservation.ChangeReservationDate(command.ReservationDate);
        await _reservationRepository.UpdateAsync(dentistIndustry);
    }
    
    private async Task<DentistIndustry> GetReservationsAsync(Guid reservationId)
    {
        var reservations = await _reservationRepository.GetAllReservationAsync();
            
        return reservations.SingleOrDefault(x => x.Reservations.Any(r => r.ReservationId == reservationId));
    }
}