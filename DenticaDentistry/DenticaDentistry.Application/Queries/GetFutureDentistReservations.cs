using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;

namespace DenticaDentistry.Application.Queries;

public class GetFutureDentistReservations : IQuery<IEnumerable<DentistReservationsDto>>
{
    public Guid UserId { get; set; }
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}