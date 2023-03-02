namespace Dentica_Dentistry.Core.Entities;

public class Reservation
{
    public int ReservationId { get; set; }
    public string ReservationName { get; set; }
    public string BookerName { get; set; }
    public DateTime ReservationDate { get; set; }

    public Reservation(int reservationId,string reservationName ,string bookerName, DateTime reservationDate)
    {
        ReservationId = reservationId;
        ReservationName = reservationName;
        BookerName = bookerName;
        ReservationDate = reservationDate;
    }
    
}