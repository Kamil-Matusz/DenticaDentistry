using DenticaDentistry.Core.Exceptions;

namespace DenticaDentistry.Core.Entities;

public class DentistIndustry
{
    private readonly HashSet<Reservation> _reservations = new();

    public int DentistIndustryId { get; private set; }
    public string Name { get; private set; }
    public double Price { get; private set; }
    public string Description { get; private set; }
    public int ServiceTypeId { get; private set; }
    public IEnumerable<Reservation> Reservations => _reservations;
    
    public DentistIndustry(int dentistIndustryId, string name, double price, string description, int serviceTypeId)
    {
        DentistIndustryId = dentistIndustryId;
        ChangeName(name);
        ChangePrice(price);
        Description = description;
        ServiceTypeId = serviceTypeId;
    }

    public void AddReservation(Reservation reservation)
    {
        var now = DateTime.UtcNow;
        if (reservation.ReservationDate <= now)
        {
            throw new InvalidReservationDateException(reservation.ReservationDate);
        }

        _reservations.Add(reservation);
    }

    public void ChangeName(string name)
    {
        Name = name;
    }

    public void ChangePrice(double price)
    {
        Price = price;
    }

    public void RemoveReservation(Guid reservationId) => _reservations.RemoveWhere(x => x.ReservationId == reservationId);
}