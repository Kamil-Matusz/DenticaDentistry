namespace Dentica_Dentistry.Application.Commands;

public record CreateReservation(Guid ReservationId, int DentistIndustryId, string BookerName, DateTime ReservationDate);
