namespace Dentica_Dentistry.Core.Entities;

public class Reservation
{
    public int ReservationId { get; set; }
    public string ReservationName { get; set; }
    public string BookerName { get; set; }
    public DateTime ReservationDate { get; set; }
}