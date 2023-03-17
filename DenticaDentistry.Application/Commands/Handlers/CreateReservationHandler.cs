using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.Exceptions;
using DenticaDentistry.Application.Services;
using DenticaDentistry.Core.Entities;
using DenticaDentistry.Core.Repositories;

namespace DenticaDentistry.Application.Commands.Handlers;

public sealed class CreateReservationHandler : ICommandHandler<CreateReservation>
{
    private readonly IClock _clock;
    private readonly IReservationRepository _reservationRepository;

    public CreateReservationHandler(IClock clock, IReservationRepository reservationRepository)
    {
        _clock = clock;
        _reservationRepository = reservationRepository;
    }

    public async Task HandlerAsync(CreateReservation command)
    {
        var dentistIndustryId = command.DentistIndustryId;
        var dentistIndustryName = await _reservationRepository.GetReservationAsync(dentistIndustryId);
        if (dentistIndustryName is null)
        {
            throw new DentistIndustryServiceNotFoundException(dentistIndustryId);
        }

        var reservation = new Reservation(command.ReservationId, command.DentistIndustryId,command.BookerName, command.ReservationDate);
        
        dentistIndustryName.AddReservation(reservation);
        await _reservationRepository.UpdateAsync(dentistIndustryName);
    }
}