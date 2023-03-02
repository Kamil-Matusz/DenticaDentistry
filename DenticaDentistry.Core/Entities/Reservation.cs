namespace Dentica_Dentistry.Core.Entities;

public class Reservation
{
    public Guid ReservationId { get; set; }
    public int DentistIndustryId { get; set; }
    public string BookerName { get; set; }
    public DateTime ReservationDate { get; set; }

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