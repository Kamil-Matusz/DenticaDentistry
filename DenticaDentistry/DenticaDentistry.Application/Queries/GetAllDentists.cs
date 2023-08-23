using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;

namespace DenticaDentistry.Application.Queries;

public class GetAllDentists : IQuery<IEnumerable<DentistDto>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}