using Dentica_Dentistry.Core.Exceptions;

namespace Dentica_Dentistry.Core.Entities;

public class DentistIndustry
{
    private readonly HashSet<Reservation> _reservations = new();

    public int DentistIndustryId { get; }
    public string Name { get; }
    public double Price { get; }
    public string Description { get; }
    public IEnumerable<Reservation> Reservations => _reservations;

    public DentistIndustry(int dentistIndustryId, string name, double price, string description)
    {
        DentistIndustryId = dentistIndustryId;
        Name = name;
        Price = price;
        Description = description;
    }

    public void AddReservation(Reservation reservation)
    {
        var now = DateTime.UtcNow.Date;
        if (reservation.ReservationDate <= now)
        {
            throw new InvalidReservationDateException(reservation.ReservationDate);
        }

        _reservations.Add(reservation);
    }

    public void RemoveReservation(Guid reservationId) =>
        _reservations.RemoveWhere(x => x.ReservationId == reservationId);
}