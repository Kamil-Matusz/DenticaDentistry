using Dentica_Dentistry.Core.Entities;

namespace Dentica_Dentistry.Application.Services;

public class ReservationsService : IReservationsService
{
    private static readonly List<Reservation> _reservations = new();

    public IEnumerable<Reservation> GetAllReservations() => _reservations;

    public Reservation GetReservation(int id)
    {
        var reservation = _reservations.SingleOrDefault(x => x.ReservationId == id);
        if (reservation is null)
        {
            return null;
        }

        return reservation;
    }

    public int? CreateReservation(Reservation reservation)
    {
        var now = DateTime.UtcNow.Date;
        var reservationId = _reservations.Any(x => x.ReservationId == reservation.ReservationId);
        if (reservationId)
        {
            return default;
        }

        if (reservation.ReservationDate.Date <= now)
        {
            return default;
        }
        
        _reservations.Add(reservation);

        return reservation.ReservationId;
    }
    
    public bool UpdateReservationDate(int id, Reservation reservation)
    {
        var existingReservation = _reservations.SingleOrDefault(x => x.ReservationId == reservation.ReservationId);
        if (existingReservation is null)
        {
            return false;
        }

        if (existingReservation.ReservationDate <= DateTime.UtcNow.Date)
        {
            return false;
        }
        
        existingReservation.ReservationDate = reservation.ReservationDate;
        return true;
    }
    
    public bool DeleteReservation(int id)
    {
        var existingReservation = _reservations.SingleOrDefault(x => x.ReservationId == id);
        if (existingReservation is null)
        {
            return false;
        }

        _reservations.Remove(existingReservation);
        return true;
    }
}