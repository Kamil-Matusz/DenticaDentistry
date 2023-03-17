using DenticaDentistry.Application.Abstractions;

namespace DenticaDentistry.Application.Commands;

public record DeleteReservation(Guid ReservationId) : ICommand;
