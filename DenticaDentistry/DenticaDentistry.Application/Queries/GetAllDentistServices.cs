using DenticaDentistry.Application.Abstractions;
using DenticaDentistry.Application.DTO;

namespace DenticaDentistry.Application.Queries;

public class GetAllDentistServices : IQuery<IEnumerable<DentistIndustryDto>>
{
    public int PageIndex { get; set; }
    public int PageSize { get; set; }
}