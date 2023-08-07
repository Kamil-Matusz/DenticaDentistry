using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;

namespace DenticaDentistry.Application.Queries;

public class GetAllUserReservations : IQuery<IEnumerable<ReservationDto>>
{
    public Guid UserId { get; set; }
}