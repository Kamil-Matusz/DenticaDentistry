using Dentica_Dentistry.Core.Entities;

namespace Dentica_Dentistry.Application.Services;

public interface IReservationsService
{
    IEnumerable<Reservation> GetAllReservations();
    Reservation GetReservation(int id);
    int? CreateReservation(Reservation reservation);
    bool UpdateReservationDate(int id, Reservation reservation);
    bool DeleteReservation(int id);
}