using Dentica_Dentistry.Application.Commands;
using Dentica_Dentistry.Application.DTO;
using Dentica_Dentistry.Core.Entities;
using Dentica_Dentistry.Core.Repositories;

namespace Dentica_Dentistry.Application.Services;

public class ReservationsService : IReservationsService
{
    private readonly IClock _clock;
    private readonly IReservationRepository _reservationRepository;

    public ReservationsService(IClock clock,IReservationRepository reservationRepository)
    {
        
        _clock = clock;
        _reservationRepository = reservationRepository;
    }
    
    public IEnumerable<ReservationDto> GetAllReservations() => _reservationRepository
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
        var dentistIndustryName = _reservationRepository.GetReservation(dentistIndustryId);
        if (dentistIndustryName is null)
        {
            return default;
        }

        var reservation = new Reservation(command.ReservationId, command.DentistIndustryId,command.BookerName, command.ReservationDate);
        
        dentistIndustryName.AddReservation(reservation);
        _reservationRepository.Update(dentistIndustryName);
        return reservation.ReservationId;
    }
    
    public bool UpdateReservationDate(ChangeReservationDate command)
    {
        var dentistIndustry = GetReservations(command.ReservationId);
        if (dentistIndustry is null)
        {
            return false;
        }

        var reservationId = command.ReservationId;
        var existingReservation = dentistIndustry.Reservations.SingleOrDefault(x => x.ReservationId == reservationId);
        if (existingReservation is null)
        {
            return false;
        }

        if (existingReservation.ReservationDate <= DateTime.UtcNow)
        {
            return false;
        }
        existingReservation.ChangeReservationDate(command.ReservationDate);
        _reservationRepository.Update(dentistIndustry);
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
       _reservationRepository.Delete(dentistIndustryId);
        return true;
    }

    private DentistIndustry GetReservations(Guid reservationId) => _reservationRepository.GetAllReservation().SingleOrDefault(x => x.Reservations.Any(r => r.ReservationId == reservationId));

}