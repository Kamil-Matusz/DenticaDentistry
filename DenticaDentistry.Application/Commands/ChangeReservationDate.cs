using DenticaDentistry.Application.Abstractions;

namespace DenticaDentistry.Application.Commands;

public record ChangeReservationDate(Guid ReservationId, DateTime ReservationDate) : ICommand;
