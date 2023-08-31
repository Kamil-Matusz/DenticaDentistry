namespace DenticaDentistry.Application.DTO;

public class DentistReservationsDto
{
    public Guid ReservationId { get; set; }
    public int DentistIndustryId { get; set; }
    public string BookerName { get; set; }
    public DateTime ReservationDate { get; set; }
}