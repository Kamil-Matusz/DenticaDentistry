namespace Dentica_Dentistry.Application.Commands;

public record ChangeReservationDate(Guid ReservationId, DateTime ReservationDate);
