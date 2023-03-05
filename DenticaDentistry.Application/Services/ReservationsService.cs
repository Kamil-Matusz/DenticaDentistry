using Dentica_Dentistry.Application.Commands;
using Dentica_Dentistry.Application.DTO;
using Dentica_Dentistry.Application.Repositories;
using Dentica_Dentistry.Core.Entities;

namespace Dentica_Dentistry.Application.Services;

public class ReservationsService : IReservationsService
{
    private readonly IClock _clock;
    private readonly IDentistIndustryRepository _dentistIndustryRepository;

    public ReservationsService(IClock clock,IDentistIndustryRepository dentistIndustryRepository)
    {
        
        _clock = clock;
        _dentistIndustryRepository = dentistIndustryRepository;
    }
    
    public IEnumerable<ReservationDto> GetAllReservations() => _dentistIndustryRepository
        .GetAllReservation()
        .SelectMany(x => x.Reservations)
        .Select(x => new ReservationDto
        {
            ReservationId = x.ReservationId,
            DentistIndustryId = x.DentistIndustryId,
            BookerName = x.BookerName,
            ReservationDate = x.ReservationDate
        });

    public ReservationDto GetReservation(Guid id)
    {
        var reservation = GetAllReservations().SingleOrDefault(x => x.ReservationId == id);
        if (reservation is null)
        {
            return null;
        }

        return reservation;
    }

    public Guid? CreateReservation(CreateReservation command)
    {
        var dentistIndustryId = command.DentistIndustryId;
        var dentistIndustryName = _dentistIndustryRepository.GetReservation(dentistIndustryId);
        if (dentistIndustryName is null)
        {
            return default;
        }

        var reservation = new Reservation(command.ReservationId, command.DentistIndustryId,command.BookerName, command.ReservationDate);
        
        dentistIndustryName.AddReservation(reservation);
        
        return reservation.ReservationId;
    }
    
    public bool UpdateReservationDate(ChangeReservationDate command)
    {
        var dentistIndustryId = GetReservations(command.ReservationId);
        if (dentistIndustryId is null)
        {
            return false;
        }

        var existingReservation =
            dentistIndustryId.Reservations.SingleOrDefault(x => x.ReservationId == command.ReservationId);
        if (existingReservation is null)
        {
            return false;
        }

        if (existingReservation.ReservationDate <= _clock.CurrentDate())
        {
            return false;
        }
        
        existingReservation.ChangeReservationDate(command.ReservationDate);
        return true;
    }
    
    public bool DeleteReservation(DeleteReservation command)
    {
        var dentistIndustryId = GetReservations(command.ReservationId);
        if (dentistIndustryId is null)
        {
            return false;
        }

        var existingReservation = dentistIndustryId.Reservations.SingleOrDefault(x => x.ReservationId == command.ReservationId);
        if (existingReservation is null)
        {
            return false;
        }

       dentistIndustryId.RemoveReservation(command.ReservationId);
        return true;
    }

    private DentistIndustry GetReservations(Guid reservationId) => _dentistIndustryRepository.GetAllReservation().SingleOrDefault(x => x.Reservations.Any(r => r.ReservationId == reservationId));

}