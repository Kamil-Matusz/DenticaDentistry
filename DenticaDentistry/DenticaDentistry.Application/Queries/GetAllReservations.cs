using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;

namespace DenticaDentistry.Application.Queries;

public class GetAllReservations : IQuery<IEnumerable<ReservationDto>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}