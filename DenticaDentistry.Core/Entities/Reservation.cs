namespace DenticaDentistry.Core.Entities;

public class Reservation
{
    public Guid ReservationId { get; private set; }
    public int DentistIndustryId { get; private set; }
    public string BookerName { get; private set; }
    public DateTime ReservationDate { get; private set; }

    public Reservation(Guid reservationId,int dentistIndustryId ,string bookerName, DateTime reservationDate)
    {
        ReservationId = reservationId;
        DentistIndustryId = dentistIndustryId;
        BookerName = bookerName;
        ChangeReservationDate(reservationDate);
    }

    public void ChangeReservationDate(DateTime date)
    {
        ReservationDate = date;
    }
    
}