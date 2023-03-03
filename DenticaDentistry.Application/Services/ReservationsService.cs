using Dentica_Dentistry.Application.Commands;
using Dentica_Dentistry.Application.DTO;
using Dentica_Dentistry.Core.Entities;

namespace Dentica_Dentistry.Application.Services;

public class ReservationsService : IReservationsService
{
    private readonly IClock _clock;
    private static readonly List<DentistIndustry> _dentistIndustriesList = new()
    {
        new DentistIndustry(1, "Lakierowanie zębow", 100.00, "Naprawa szkliwa zębów"),
        new DentistIndustry(2, "Czyszczenie Kamienia", 200.00, "Czyszczenie zębów z nasady kamieniowej"),
        new DentistIndustry(3, "Leczenie Kanałowe", 100.00, "Usuwanie obumarłych fragmentów zębów"),
        new DentistIndustry(4, "Leczenie Próchnicy", 100.00, "Usuwanie prochnicy z zęba"),
        new DentistIndustry(5, "Wizyta Kontrolna", 50.00, "Kontrolne badanie zębów"),
    };

    public ReservationsService(IClock clock)
    {
        _clock = clock;
    }
    
    public IEnumerable<ReservationDto> GetAllReservations() => _dentistIndustriesList
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
        var dentistIndustryName = _dentistIndustriesList.SingleOrDefault(x => x.DentistIndustryId == command.DentistIndustryId);
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

    private DentistIndustry GetReservations(Guid reservationId) => _dentistIndustriesList.SingleOrDefault(x => x.Reservations.Any(r => r.ReservationId == reservationId));

}