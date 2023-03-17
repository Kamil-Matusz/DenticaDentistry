using DenticaDentistry.Application.Abstractions;

namespace DenticaDentistry.Application.Commands;

public record CreateReservation(Guid ReservationId, int DentistIndustryId, string BookerName, DateTime ReservationDate)  : ICommand;
