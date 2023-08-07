using System.Collections;
using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;

namespace DenticaDentistry.Application.Queries;

public class GetFutureUserReservations : IQuery<IEnumerable<ReservationDto>>
{
    public Guid UserId { get; set; }
}